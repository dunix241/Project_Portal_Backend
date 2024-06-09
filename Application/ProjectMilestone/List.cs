using Application.Core;
using Application.ProjectMilestone.DTOs;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectMilestone
{
    public class List
    {
        public class Query : IRequest<Result<ListProjectMilestoneResponseDto>>
        {
            public ListProjectMilestoneRequestDto query { get; set; }
            public PagingParams pagination { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ListProjectMilestoneResponseDto>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<ListProjectMilestoneResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var name = request.query.Name;
                var schoolId = request.query.SchoolId;

                var query = _context.ProjectMilestones.AsQueryable()
                    .Where(x => (name == null || x.Name.ToLower().Contains(name.ToLower()))
                                && (schoolId == null || x.SchoolId == schoolId));

                var projects = new ListProjectMilestoneResponseDto();
                await projects.GetItemsAsync(query, request.pagination.PageNumber, request.pagination.PageSize);

                return Result<ListProjectMilestoneResponseDto>.Success(projects);
            }
        }
    }
}
