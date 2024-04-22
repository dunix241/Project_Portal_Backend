using Application.Core;
using Application.Schools.DTOs;
using MediatR;
using Persistence;

namespace Application.Schools
{
    public class List
    {
        public class Query : IRequest<Result<ListSchoolResponseDto>>
        {
            public PagingParams QueryParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ListSchoolResponseDto>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<ListSchoolResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Schools
                   .Where(s => s.IsActive)
                   .AsQueryable();

                var schools = new ListSchoolResponseDto();
                await schools.GetItemsAsync(query, request.QueryParams.PageNumber, request.QueryParams.PageSize);

                return Result<ListSchoolResponseDto>.Success(schools);
            }
        }
    }
}
