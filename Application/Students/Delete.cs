using Application.Core;
using AutoMapper;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using MediatR;
using Persistence;

namespace Application.Students
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
                var student = await _context.Students.FindAsync(request.Id);

                if (student == null)
                {
                    return null;
                }
                var user = await _context.Users.FindAsync(request.Id);
                if (user != null)
                {
                    user.IsActive = false;
                }
                else
                {
                    return null;
                }


                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
