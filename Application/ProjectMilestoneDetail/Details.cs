using Application.Core;
using Application.Projects.DTOs;
using MediatR;
using Persistence;
using Project = Domain.Project;

namespace Application.ProjectMilestoneDetail
{

    public class Details
    {
        public class Query : IRequest<Result<Project.ProjectMilestoneDetails>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Project.ProjectMilestoneDetails>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Project.ProjectMilestoneDetails>> Handle(Query request, CancellationToken cancellationToken)
            {
                var project = await _context.Projects.FindAsync(request.Id);
                if (project == null)
                {
                    return null;
                }
                return Result<Project.ProjectMilestoneDetails>.Success(new Project.ProjectMilestoneDetails { Project = project });
            }
        }
    }
}