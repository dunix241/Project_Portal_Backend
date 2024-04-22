using Application.Core;
using MediatR;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Application.Minio
{
    public class DownloadFile
    {
        public class Query : IRequest<Result<string>>
        {
            public string BucketName { get; set; }
            public string ObjectName { get; set; }
            public string FilePath { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<string>>
        {
            private readonly IMinioClient _minioClient;

            public Handler(IMinioClient minioClient)
            {
                _minioClient = minioClient;
            }

            public async Task<Result<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var statArgs = new StatObjectArgs().WithBucket(request.BucketName).WithObject(request.ObjectName);
                    await _minioClient.StatObjectAsync(statArgs);

                    var presignedArg = new PresignedGetObjectArgs()
                                        .WithBucket(request.BucketName)
                                        .WithObject(request.ObjectName)
                                        .WithExpiry(5);

                    var presignedUrl = await _minioClient.PresignedGetObjectAsync(presignedArg).ConfigureAwait(false);
                    return Result<string>.Success(presignedUrl);
                }
                catch (MinioException ex)
                {
                    return Result<string>.Failure($"Error downloading file: {ex.Message}");
                }
            }

        }
    }
}
