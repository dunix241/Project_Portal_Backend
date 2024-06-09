using Application.Core;
using Application.ProjectEnrollmentMember.DTOs;
using Application.Semesters.DTOs;
using MediatR;
using Persistence;
using Project = Domain.Project;

namespace Application.ProjectEnrollmentMember;

public class Details
{
    public class Query : IRequest<Result<GetEnrollmentMemberDto>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<GetEnrollmentMemberDto>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<GetEnrollmentMemberDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var semester = await _context.ProjectEnrollmentMembers.FindAsync(request.Id);
            if (semester == null) return null;
            return Result<GetEnrollmentMemberDto>.Success(new GetEnrollmentMemberDto { ProjectEnrollmentMember = semester });
        }
    }
}