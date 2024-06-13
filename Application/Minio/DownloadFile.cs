using Application.Core;
using MediatR;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Persistence;

namespace Application.Minio
{
    public class DownloadFile
    {
        public class Query : IRequest<Result<string>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<string>>
        {
            private readonly IMinioClient _minioClient;
            private readonly DataContext _dataContext;

            public Handler(IMinioClient minioClient, DataContext dataContext)
            {
                _minioClient = minioClient;
                _dataContext = dataContext;
            }

            public async Task<Result<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                var file = await _dataContext.Files.FindAsync(request.Id);
                if (file == null) return null;
                
                try
                {
                    var statArgs = new StatObjectArgs().WithBucket(file.BucketName).WithObject(file.FileNameWithExtension);
                    await _minioClient.StatObjectAsync(statArgs);

                    var presignedArg = new PresignedGetObjectArgs()
                                        .WithBucket(file.BucketName)
                                        .WithObject(file.FileNameWithExtension)
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
