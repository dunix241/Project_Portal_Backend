using Application.Core;
using MediatR;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Application.Minio
{
    public class StreamFile
    {
        public class Query : IRequest<Result<Stream>> // Adjusted the result type to Stream
        {
            public string BucketName { get; set; }
            public string ObjectName { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Stream>>
        {
            private readonly IMinioClient _minioClient;

            public Handler(IMinioClient minioClient)
            {
                _minioClient = minioClient;
            }

            public async Task<Result<Stream>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var statArgs = new StatObjectArgs().WithBucket(request.BucketName).WithObject(request.ObjectName);
                    await _minioClient.StatObjectAsync(statArgs);

                    var memoryStream = new MemoryStream();
                    var arg = new GetObjectArgs()
                                        .WithBucket(request.BucketName)
                                        .WithObject(request.ObjectName)
                                        .WithCallbackStream((stream) => stream.CopyTo(memoryStream));

                    await _minioClient.GetObjectAsync(arg);

                    memoryStream.Seek(0, SeekOrigin.Begin); // Reset stream position before returning

                    return Result<Stream>.Success(memoryStream);
                }
                catch (MinioException ex)
                {
                    return Result<Stream>.Failure($"Error downloading file: {ex.Message}");
                }
            }
        }
    }
}
