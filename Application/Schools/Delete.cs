using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Schools
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
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var school = await _context.Schools.FindAsync(request.Id);
                if (school == null)
                {
                    return null;
                }
                school.IsActive = false;

                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
