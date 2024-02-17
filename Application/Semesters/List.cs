using Application.Core;
using Application.Semesters.DTOs;
using MediatR;
using Persistence;

namespace Application.Semesters;

public class List
{
    public class Query : IRequest<Result<ListSemesterResponseDto>>
    {
        public PagingParams Params { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<ListSemesterResponseDto>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<ListSemesterResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Semesters.AsQueryable();

            var semesters = new ListSemesterResponseDto();
            await semesters.GetItemsAsync(query, request.Params.PageNumber, request.Params.PageSize);

            return Result<ListSemesterResponseDto>.Success(semesters);
        }
    }
}