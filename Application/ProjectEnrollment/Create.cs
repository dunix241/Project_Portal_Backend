using Application.Core;
using Application.ProjectEnrollment.DTOs;
using Application.ProjectMilestone.DTOs;
using AutoMapper;
using Domain.Project;
using MediatR;
using Persistence;
using Project = Domain.Project;

namespace Application.ProjectEnrollment
{
    public class Create
    {
        public class Command : IRequest<Result<Project.ProjectEnrollment >>
        {
            public CreateProjectEnrollmentRequestDto dto { get; set; }
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
                var projecEnreollment = new Domain.Project.ProjectEnrollment();
                _mapper.Map(request.dto, projecEnreollment);
                _context.ProjectEnrollments.Add(projecEnreollment);
                await _context.SaveChangesAsync();

                return Result<Project.ProjectEnrollment>.Success(projecEnreollment);
            }
        }
    }
}
