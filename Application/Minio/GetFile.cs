using Application.Core;
using Application.Minio.DTOs;
using AutoMapper;
using MediatR;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Persistence;
using File = Domain.File.File;

namespace Application.Minio
{
    public class GetFile
    {
        public class Query : IRequest<Result<FileResponseDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<FileResponseDto>>
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

            public async Task<Result<FileResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                File file = await _context.Files.FindAsync(request.Id);
                if (file == null) return null;
                
                try
                {
                    var url = await _minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs()
                        .WithBucket(file.BucketName)
                        .WithObject(file.FileNameWithExtension)
                        .WithExpiry(60 * 60 * 2));

                    var response = _mapper.Map<FileResponseDto>(file);
                    response.Url = url;

                    return Result<FileResponseDto>.Success(response);
                }
                catch (MinioException ex)
                {
                    return Result<FileResponseDto>.Failure($"Error getting file: {ex.Message}");
                }
            }
        }
    }
}
