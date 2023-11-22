using Application.Core;
using MediatR;
using Persistence;

namespace Application.MockDomains;

public class Delete
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
    
    public class Handler: IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var mockDomain = await _context.MockDomains.FindAsync(request.Id);
            if (mockDomain == null)
            {
                return null;
            }

            _context.Remove(mockDomain);
            await _context.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}