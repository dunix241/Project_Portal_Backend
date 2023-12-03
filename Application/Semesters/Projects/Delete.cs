using Application.Core;
using MediatR;
using Persistence;

namespace Application.Semesters.Projects;

public class Delete
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid SemesterId { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var projectSemester = await _context.ProjectSemesters.FindAsync(request.ProjectId, request.SemesterId);

            if (projectSemester == null) return null;

            _context.Remove(projectSemester);
            await _context.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}