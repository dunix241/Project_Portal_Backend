using Application.Core;
using Application.Minio.DTOs;
using MediatR;
using Minio.DataModel.Args;
using Minio;
using Persistence;
using File = Domain.File;
using Microsoft.EntityFrameworkCore;
using Domain.Student;
using AutoMapper;
using Domain.Enum;

namespace Application.Minio
{
    public class UploadFile_V2
    {
        public class Command : IRequest<Result<AddFileResponseDto>>
        {
            public AddFileRequestDto dto;
        }

        public class Handler : IRequestHandler<Command, Result<AddFileResponseDto>>
        {
            private readonly IMinioClient _minioClient;
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(IMinioClient minioClient, DataContext context, IMapper mapper)
            {
                _minioClient = minioClient;
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<AddFileResponseDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var file = request.dto.FormFile;
                    var bucketName = request.dto.BucketName;
                    var sourceOwnerType = request.dto.SourceOwnerType;
                    var sourceOwnerId = request.dto.SourceOwnerId;
                    var fileType = request.dto.FileType;

                    if (file.Length <= 0)
                    {
                        return Result<AddFileResponseDto>.Failure("No file uploaded or file is empty.");
                    }


                    var existArgs = new BucketExistsArgs().WithBucket(request.dto.BucketName);
                    var found = await _minioClient.BucketExistsAsync(existArgs).ConfigureAwait(false);
                    if (!found)
                    {
                        return Result<AddFileResponseDto>.Failure($"Bucket {request.dto.BucketName} does not exist.");
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        DateTime currentTime = DateTime.UtcNow;
                        var extension = Path.GetExtension(file.FileName);
                        var unixTime = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName) + $"_{unixTime}{extension}";

                        var fileObj = new File.File
                        {
                            FileOriginalName = file.FileName,
                            FileName = fileName,
                            FileType = fileType,
                        };

                        

                        switch (sourceOwnerType)
                        {
                            case SourceOwnerType.Student:
                                var student = await _context.Students.FindAsync(sourceOwnerId);
                                if (student == null)
                                    return Result<AddFileResponseDto>.Failure($"Student ID {sourceOwnerId} does not exist.");
                                fileObj.Student = student;
                                fileObj.StudentId = sourceOwnerId;

                                if (fileType == File.FileType.Avatar)
                                {
                                    var existFile = await _context.Files.Where(x => (x.StudentId == sourceOwnerId && x.FileType == File.FileType.Avatar)).FirstOrDefaultAsync();
                                    if(existFile!= null)
                                    {
                                        existFile.FileType = File.FileType.Img;
                                    }
                                }

                                break;

                            case SourceOwnerType.Lecturer:
                                var lecturer = await _context.Lecturers.FindAsync(sourceOwnerId);
                                if (lecturer == null)
                                    return Result<AddFileResponseDto>.Failure($"Lecturer ID {sourceOwnerId} does not exist.");
                                fileObj.Lecturer = lecturer;
                                fileObj.LecturerId = sourceOwnerId;

                                if (fileType == File.FileType.Avatar)
                                {
                                    var existFile = await _context.Files.Where(x => (x.LecturerId == sourceOwnerId && x.FileType == File.FileType.Avatar)).FirstOrDefaultAsync();
                                    if (existFile != null)
                                    {
                                        existFile.FileType = File.FileType.Img;
                                    }
                                }
                                break;

                            case SourceOwnerType.Project:
                                var project = await _context.Projects.FindAsync(sourceOwnerId);
                                if (project == null)
                                    return Result<AddFileResponseDto>.Failure($"Project ID {sourceOwnerId} does not exist.");
                                fileObj.Project = project;
                                fileObj.ProjectId = sourceOwnerId;
                                break;

                            default:
                                return Result<AddFileResponseDto>.Failure($"Error searching for owner target");
                        }


                        await file.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;

                        var putArgs = new PutObjectArgs()
                            .WithBucket(request.dto.BucketName)
                            .WithObject(fileName)
                            .WithStreamData(memoryStream)
                            .WithObjectSize(memoryStream.Length)
                            .WithContentType("application/octet-stream")
                          ;
                        await _minioClient.PutObjectAsync(putArgs);
                        _context.Files.Add(fileObj);
                        var success = await _context.SaveChangesAsync() != 0;
                        if (!success)
                        {
                            return Result<AddFileResponseDto>.Failure($"Error adding File Object to DB");
                        }
                        var respone = new AddFileResponseDto();
                        _mapper.Map(fileObj, respone );
                        return Result<AddFileResponseDto>.Success( respone);

                    }
                }
                catch (Exception ex)
                {
                    return Result<AddFileResponseDto>.Failure($"Error uploading file: {ex.Message}");
                }
            }
        }
    }
}
