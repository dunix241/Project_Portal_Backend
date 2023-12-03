using Application.Core;
using Application.Semesters.DTOs.Projects;
using MediatR;
using Persistence;

namespace Application.Semesters.Projects;

public class List
{
    public class Query : IRequest<Result<SemesterListProjectResponseDto>>
    {
        public Guid SemesterId { get; set; }
        public PagingParams Params { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<SemesterListProjectResponseDto>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<SemesterListProjectResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.ProjectSemesters
                .Where(projectSemester => projectSemester.SemesterId == request.SemesterId)
                .AsQueryable();
            var projectSemesters = new SemesterListProjectResponseDto();
            await projectSemesters.GetItemsAsync(query, request.Params.PageNumber, request.Params.PageSize);
            return Result<SemesterListProjectResponseDto>.Success(projectSemesters);
        }
    }
}