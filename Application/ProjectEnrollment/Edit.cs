using Application.Core;
using Application.ProjectEnrollment.DTOs;
using Application.ProjectMilestone.DTOs;
using AutoMapper;
using MediatR;
using Persistence;
using Project = Domain.Project;

namespace Application.ProjectEnrollment
{
    public class Edit
    {
        public class Command : IRequest<Result<Project.ProjectEnrollment>>
        {
            public Guid Id { get; set; }
            public EditProjectEnrollmentRequestDto dto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Project.ProjectEnrollment>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Project.ProjectEnrollment>> Handle(Command request, CancellationToken cancellationToken)
            {
                var project = await _context.ProjectEnrollments.FindAsync(request.Id);
                if (project == null)
                {
                    Result<Project.ProjectEnrollment>.Failure("Not found");
                }

                _mapper.Map(request.dto, project);

                await _context.SaveChangesAsync();


                return Result<Project.ProjectEnrollment>.Success(project);
            }
        }
    }
}
