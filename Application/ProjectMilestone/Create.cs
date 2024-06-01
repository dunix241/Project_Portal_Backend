using Application.Core;
using Application.ProjectMilestone.DTOs;
using AutoMapper;
using Domain.Project;
using MediatR;
using Persistence;
using Project = Domain.Project;

namespace Application.ProjectMilestone
{
    public class Create
    {
        public class Command : IRequest<Result<Project.ProjectMilestone>>
        {
            public CreateProjectMilestoneRequestDto dto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Project.ProjectMilestone>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Project.ProjectMilestone>> Handle(Command request, CancellationToken cancellationToken)
            {
                var projectMilestone = new Domain.Project.ProjectMilestone();
                _mapper.Map(request.dto, projectMilestone);
                _context.ProjectMilestones.Add(projectMilestone);
                await _context.SaveChangesAsync();

                return Result<Project.ProjectMilestone>.Success(projectMilestone);
            }
        }
    }
}
