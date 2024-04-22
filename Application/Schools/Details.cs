using Application.Core;
using Application.Schools.DTOs;
using MediatR;
using Persistence;

namespace Application.Schools
{
    public class Details
    {
        public class Query : IRequest<Result<GetSchoolResponseDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<GetSchoolResponseDto>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<GetSchoolResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var school = await _context.Schools.FindAsync(request.Id);
                if (school == null)
                {
                    return null;
                }
                return Result<GetSchoolResponseDto>.Success(new GetSchoolResponseDto { School = school });
            }
        }
    }
}
