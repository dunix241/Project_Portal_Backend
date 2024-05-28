using Application.Core;
using Application.Minio.DTOs;
using Application.Schools.DTOs;
using AutoMapper;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Minio
{
    public class EditFile
    {
        public class Command : IRequest<Result<GetFileResponseDto>>
        {
            public Guid Id { get; set; }
            public EditFileRequestDto Dto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<GetFileResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<GetFileResponseDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var file = await _context.Files.FindAsync(request.Id);
                if (file == null)
                {
                    return Result<GetFileResponseDto>.Failure("Not found");
                }

                _mapper.Map(request.Dto, file);

                await _context.SaveChangesAsync();

                return Result<GetFileResponseDto>.Success(new GetFileResponseDto { File = file });
            }
        }
    }
}
