using Application.Core;
using MediatR;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Persistence;

namespace Application.Minio
{
    public class DeleteFile
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IMinioClient _minioClient;
            private readonly DataContext _dataContext;

            public Handler(IMinioClient minioClient, DataContext dataContext)
            {
                _minioClient = minioClient;
                _dataContext = dataContext;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var file = await _dataContext.Files.FindAsync(request.Id);
                if (file == null) return null;
                    
                try
                {
                    var statArgs = new StatObjectArgs().WithBucket(file.BucketName).WithObject(file.FileNameWithExtension);
                    ObjectStat objectStat = await _minioClient.StatObjectAsync(statArgs);

                    var removeArgs = new RemoveObjectArgs()
                        .WithBucket(file.BucketName)
                        .WithObject(file.FileNameWithExtension);

                    await _minioClient.RemoveObjectAsync(removeArgs);

                    _dataContext.Remove(file);
                    
                    return (await _dataContext.SaveChangesAsync()) != 0 ?  Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Error deleting file");
                }
                catch (Exception ex)
                {
                    return Result<Unit>.Failure($"Error deleting file: {ex.Message}");
                }
            }
        }
    }

}
