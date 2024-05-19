using Application.Core;
using Application.Lecturers.DTOs;
using AutoMapper;
using Domain.Lecturer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Lecturers
{
    public class Details
    {
        public class Query : IRequest<Result<GetLecturerResponseDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<GetLecturerResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<GetLecturerResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var lecturer = await _context.Lecturers
                   .Where(s => s.IsActive)
                   .Where(s => s.Id == request.Id)
                   .FirstOrDefaultAsync();


                if (lecturer == null)
                {
                    return Result<GetLecturerResponseDto>.Failure("Lecturer not found.");
                }

                var responseDto = _mapper.Map<GetLecturerResponseDto>(lecturer);

                return Result<GetLecturerResponseDto>.Success(responseDto);
            }
        }
    }
}
