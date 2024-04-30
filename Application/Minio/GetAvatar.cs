using Application.Core;
using MediatR;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.File;

using Application.Minio.DTOs;
using File = Domain.File.File;

namespace Application.Minio
{
    public class GetAvatar
    {
        public class Query : IRequest<Result<string>>
        {
            public string BucketName { get; set; }
            public Guid OwnerId { get; set; }
            public SourceOwnerType OwnerType { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<string>>
        {
            private readonly IMinioClient _minioClient;
            private readonly DataContext _context;

            public Handler(IMinioClient minioClient, DataContext context)
            {
                _minioClient = minioClient;
                _context = context;
            }

            public async Task<Result<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    File file = null;


                    switch (request.OwnerType)
                    {
                        case SourceOwnerType.Student:
                            file = await _context.Files.FirstOrDefaultAsync(x =>
                                x.FileType == FileType.Avatar && x.StudentId == request.OwnerId);
                            break;

                        case SourceOwnerType.Lecturer:
                            file = await _context.Files.FirstOrDefaultAsync(x =>
                                x.FileType == FileType.Avatar && x.LecturerId == request.OwnerId);
                            break;

                        case SourceOwnerType.Project:
                            file = await _context.Files.FirstOrDefaultAsync(x =>
                                x.FileType == FileType.Avatar && x.ProjectId == request.OwnerId);
                            break;

                        default:
                            return Result<string>.Failure($"Unsupported owner type: {request.OwnerType}");
                    }

                    if (file == null)
                        return Result<string>.Failure($"No avatar found for {request.OwnerType} with ID {request.OwnerId}");

                    var url = await _minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs()
                        .WithBucket(request.BucketName)
                        .WithObject(file.FileName)
                        .WithExpiry(60 * 60 * 2));

                    return Result<string>.Success(url);
                }
                catch (MinioException ex)
                {
                    return Result<string>.Failure($"Error getting Avatar URL: {ex.Message}");
                }
            }
        }
    }
}
