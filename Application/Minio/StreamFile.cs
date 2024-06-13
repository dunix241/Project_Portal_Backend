using Application.Core;
using Application.Minio.DTOs;
using MediatR;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Persistence;

namespace Application.Minio
{
    public class StreamFile
    {
        public class Query : IRequest<Result<StreamFileResponseDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<StreamFileResponseDto>>
        {
            private readonly IMinioClient _minioClient;
            private readonly DataContext _dataContext;

            public Handler(IMinioClient minioClient, DataContext dataContext)
            {
                _minioClient = minioClient;
                _dataContext = dataContext;
            }

            public async Task<Result<StreamFileResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var file = await _dataContext.Files.FindAsync(request.Id);
                if (file == null) return null;
                
                try
                {
                    var statArgs = new StatObjectArgs().WithBucket(file.BucketName).WithObject(file.FileNameWithExtension);
                    await _minioClient.StatObjectAsync(statArgs);

                    var memoryStream = new MemoryStream();
                    var arg = new GetObjectArgs()
                                        .WithBucket(file.BucketName)
                                        .WithObject(file.FileNameWithExtension)
                                        .WithCallbackStream((stream) => stream.CopyTo(memoryStream));

                    await _minioClient.GetObjectAsync(arg);

                    memoryStream.Seek(0, SeekOrigin.Begin); // Reset stream position before returning

                    var response = new StreamFileResponseDto { File = file, Stream = memoryStream };
                    return Result<StreamFileResponseDto>.Success(response);
                }
                catch (MinioException ex)
                {
                    return Result<StreamFileResponseDto>.Failure($"Error downloading file: {ex.Message}");
                }
            }
        }
    }
}
