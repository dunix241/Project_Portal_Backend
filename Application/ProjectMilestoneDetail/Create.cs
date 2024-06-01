using Application.Core;
using Application.ProjectMilestone.DTOs;
using AutoMapper;
using MediatR;
using Persistence;
using Project = Domain.Project;

namespace Application.ProjectMilestoneDetail
{
    public class Create
    {
        public class Command : IRequest<Result<Project.ProjectMilestoneDetails>>
        {
            public CreateProjectMilestoneRequestDto dto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Project.ProjectMilestoneDetails>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Project.ProjectMilestoneDetails>> Handle(Command request, CancellationToken cancellationToken)
            {
                var projectMilestone = new Domain.Project.ProjectMilestoneDetails();
                _mapper.Map(request.dto, projectMilestone);
                _context.ProjectMilestoneDetails.Add(projectMilestone);
                await _context.SaveChangesAsync();

                return Result<Project.ProjectMilestoneDetails>.Success(projectMilestone);
            }
        }
    }
}
