using Application.Core;
using Application.Students.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Students
{
    public class Details
    {
        public class Query : IRequest<Result<GetStudentResponseDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<GetStudentResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<GetStudentResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var student = await _context.Students
                    .Where(s => s.IsActive)
                    .Include(s => s.School)
                    .Where(s => s.Id == request.Id && s.School.IsActive)
                    .FirstOrDefaultAsync();

                if (student == null)
                {
                    return Result<GetStudentResponseDto>.Failure("Lecturer not found.");
                }

                var responseDto = GetStudentResponseDto.FromStudent(student);

                return Result<GetStudentResponseDto>.Success(responseDto);
            }
        }
    }
}
