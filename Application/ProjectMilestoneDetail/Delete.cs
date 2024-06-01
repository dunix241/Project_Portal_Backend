using Application.Core;
using MediatR;
using Persistence;
namespace Application.ProjectMilestoneDetail
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
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
                var project = await _context.ProjectMilestoneDetails.FindAsync(request.Id);
                if (project == null)
                {
                    return null;
                }

                _context.Remove(project);
                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
