using Application.Core;
using MediatR;
using Minio;
using Minio.DataModel.Args;

namespace Application.Minio
{
    public class UploadFile
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string FilePath { get; set; }
            public string BucketName { get; set; }
            public string ObjectName { get; set; }
            public string NewName { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IMinioClient _minioClient;

            public Handler( IMinioClient minioClient)
            {
                _minioClient = minioClient;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    // Check if the bucket exists
                    var existArgs = new BucketExistsArgs().WithBucket(request.BucketName);
                    var found = await _minioClient.BucketExistsAsync(existArgs).ConfigureAwait(false);
                    if (!found)
                    {
                        return Result<Unit>.Failure($"Bucket {request.BucketName} does not exist.");
                    }

                    // Upload the file to MinIO
                    var putArgs = new PutObjectArgs()
                        .WithBucket(request.BucketName)
                        .WithObject(request.NewName)
                        .WithContentType("application/octet-stream")
                        .WithFileName(request.FilePath);

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
