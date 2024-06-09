using Application.Core;
using Application.ProjectEnrollment.DTOs;
using Application.ProjectMilestone.DTOs;
using MediatR;
using Persistence;
using Project = Domain.Project;

namespace Application.ProjectEnrollment
{
    public class Detail
    {
        public class Query : IRequest<Result<GetProjectEnrollmentResposneDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<GetProjectEnrollmentResposneDto>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<GetProjectEnrollmentResposneDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var project = await _context.ProjectEnrollments.FindAsync(request.Id);
                if (project == null)
                {
                    return null;
                }
                return Result<GetProjectEnrollmentResposneDto>.Success( new GetProjectEnrollmentResposneDto { ProjectEnrollment = project} );
            }
        }
    }
}
