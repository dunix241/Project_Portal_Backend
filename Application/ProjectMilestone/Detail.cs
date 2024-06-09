using Application.Core;
using Application.ProjectMilestone.DTOs;
using Domain.Project;
using MediatR;
using Persistence;
using Project = Domain.Project;

namespace Application.ProjectMilestone
{
    public class Detail
    {
        public class Query : IRequest<Result<GetProjectMilestoneResponseDto>>  
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<GetProjectMilestoneResponseDto>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<GetProjectMilestoneResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var project = await _context.ProjectMilestones.FindAsync(request.Id);
                if (project == null)
                {
                    return null;
                }
                return Result<GetProjectMilestoneResponseDto>.Success(new GetProjectMilestoneResponseDto { ProjectMilestone = project});
            }
        }
    }
}
