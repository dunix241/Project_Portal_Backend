using Application.Core;
using MediatR;
using Minio.DataModel.Args;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.AppSetting;
using Microsoft.Extensions.Options;
using Minio.DataModel;
using System.Security.AccessControl;

namespace Application.Minio
{
    public class DeleteFile
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string BucketName { get; set; }
            public string ObjectName { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IMinioClient _minioClient;

            public Handler(IOptions<MinioSetting> minionConfig, IMinioClient minioClient)
            {
                _minioClient = minioClient;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var flag = true;
                    try
                    {
                        var statArgs = new StatObjectArgs().WithBucket(request.BucketName).WithObject(request.ObjectName);
                        ObjectStat objectStat = await _minioClient.StatObjectAsync(statArgs);
                    }
                    catch (Exception ex)
                    {
                        flag = false;
                    }

                    if (!flag)
                    {
                        return Result<Unit>.Failure("File not found");
                    }

                    var removeArgs = new RemoveObjectArgs()
                        .WithBucket(request.BucketName)
                        .WithObject(request.ObjectName);

                    await _minioClient.RemoveObjectAsync(removeArgs);

                    return Result<Unit>.Success(Unit.Value);
                }
                catch (Exception ex)
                {
                    // Log the error or handle it as needed
                    return Result<Unit>.Failure($"Error deleting file: {ex.Message}");
                }
            }
        }
    }

}
