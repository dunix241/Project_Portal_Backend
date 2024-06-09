using Application.Core;
using Application.ProjectEnrollmentMember.DTOs;
using AutoMapper;
using MediatR;
using Persistence;
using Project = Domain.Project;
namespace Application.ProjectEnrollmentMember;

public class Create
{
    public class Command : IRequest<Result<Project.ProjectEnrollmentMember>>
    {
        public CreateProjectEnrollmentMemberRequest Dto { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Project.ProjectEnrollmentMember>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Project.ProjectEnrollmentMember>> Handle(Command request, CancellationToken cancellationToken)
        {
             var member = new Project.ProjectEnrollmentMember();
             _mapper.Map(request.Dto, member);

             _context.ProjectEnrollmentMembers.Add(member);
             await _context.SaveChangesAsync();

             return Result<Project.ProjectEnrollmentMember>.Success(member);
        }
    }
}