using Application.Core;
using Application.Minio.DTOs;
using AutoMapper;
using MediatR;
using Minio;
using Minio.DataModel.Args;
using Persistence;
using File = Domain.File.File;

namespace Application.Minio
{
    public class UploadFile
    {
        public class Command : IRequest<Result<AddFileResponseDto>>
        {
            public AddFileRequestDto Payload;
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
                    var file = request.Payload.FormFile;
                    var bucketName = request.Payload.BucketName;

                    if (file.Length <= 0)
                    {
                        return Result<AddFileResponseDto>.Failure("No file uploaded or file is empty.");
                    }

                    var existArgs = new BucketExistsArgs().WithBucket(request.Payload.BucketName);
                    var found = await _minioClient.BucketExistsAsync(existArgs).ConfigureAwait(false);
                    if (!found)
                    {
                        return Result<AddFileResponseDto>.Failure($"Bucket {request.Payload.BucketName} does not exist.");
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        var extension = Path.GetExtension(file.FileName);
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName);

                        var fileObj = new File
                        {
                            Name = Guid.NewGuid(),
                            DisplayName = fileName,
                            Extension = extension,
                            BucketName = bucketName
                        };
                        
                        await file.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;

                        var putArgs = new PutObjectArgs()
                            .WithBucket(request.Payload.BucketName)
                            .WithObject(fileObj.Name.ToString())
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
                        return Result<AddFileResponseDto>.Success(respone);

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
