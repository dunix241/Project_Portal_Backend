using Application.Core;
using Application.Semesters.DTOs;
using MediatR;
using Persistence;

namespace Application.Semesters;

public class Details
{
    public class Query : IRequest<Result<GetSemesterResponseDto>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<GetSemesterResponseDto>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<GetSemesterResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var semester = await _context.Semesters.FindAsync(request.Id);
            if (semester == null) return null;
            return Result<GetSemesterResponseDto>.Success(new GetSemesterResponseDto { Semester = semester });
        }
    }
}