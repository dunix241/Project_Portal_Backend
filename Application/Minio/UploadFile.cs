using Application.Core;
using Application.Core.AppSetting;
using Application.Lecturers.DTOs;
using Application.Lecturers.Validation;
using Application.Minio.DTOs;
using AutoMapper;
using DocumentFormat.OpenXml.InkML;
using Domain.Lecturer;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using MinioSetting = Application.Core.AppSetting.MinioSetting;

namespace Application.Minio
{
    public class UploadFile
    {
        public class Command : IRequest<Result<Unit>>
        {
           public AddFileRequestDto dto;
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IMinioClient _minioClient;
            private readonly DataContext _context;

            public Handler( IMinioClient minioClient,DataContext context)
            {
                _minioClient = minioClient;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    // Check if the bucket exists
                    var existArgs = new BucketExistsArgs().WithBucket(request.dto.BucketName);
                    var found = await _minioClient.BucketExistsAsync(existArgs).ConfigureAwait(false);
                    if (!found)
                    {
                        return Result<Unit>.Failure($"Bucket {request.dto.BucketName} does not exist.");
                    }

                    // Upload the file to MinIO
                    var putArgs = new PutObjectArgs()
                        .WithBucket(request.dto.BucketName)
                        .WithObject(request.dto.NewName + request.dto.Extension)
                        .WithContentType("application/octet-stream")
                        .WithFileName(request.dto.FilePath);




                    _ = await _minioClient.PutObjectAsync(putArgs).ConfigureAwait(false);




                    return Result<Unit>.Success(Unit.Value);
                }
                catch (Exception ex)
                {
                    return Result<Unit>.Failure($"Error uploading file: {ex.Message}");
                }
            }
        }

    }
}
