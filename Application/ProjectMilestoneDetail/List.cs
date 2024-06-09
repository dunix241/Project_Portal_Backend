using Application.Core;
using Application.ProjectMilestoneDetail.DTOs;
using MediatR;
using Persistence;


namespace Application.ProjectMilestoneDetail
{
    public class List
    {
        public class Query : IRequest<Result<ListProjectMilestoneDetailResponse>>
        {
            public ListProjectMilestoneDetailRequest query { get; set; }
            public PagingParams pagination { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ListProjectMilestoneDetailResponse>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<ListProjectMilestoneDetailResponse>> Handle(Query request, CancellationToken cancellationToken)
            {

                var projectId = request.query.ProjectId;
                var id = request.query.Id;
                var projectMilestoneId = request.query.ProjectMilestoneId;

                var query = _context.ProjectMilestoneDetailses.AsQueryable()
                    .Where(x => (projectId == null || x.ProjectId == projectId)
                                && (id == null || x.Id == id)
                                && (projectMilestoneId == null || x.ProjectMilestoneId == projectMilestoneId));

                var projects = new ListProjectMilestoneDetailResponse();
                await projects.GetItemsAsync(query, request.pagination.PageNumber, request.pagination.PageSize);

                return Result<ListProjectMilestoneDetailResponse>.Success(projects);
            }
        }
    }
}
