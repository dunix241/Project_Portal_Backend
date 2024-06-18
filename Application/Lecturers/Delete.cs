using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Lecturers
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
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
                var lecturer = await _context.Lecturers.FindAsync(request.Id);
                if (lecturer == null)
                {
                    return null;
                }
                lecturer.IsActive = false;

                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
