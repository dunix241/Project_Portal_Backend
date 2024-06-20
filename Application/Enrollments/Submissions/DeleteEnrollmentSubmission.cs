using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Enrollments.Submissions
{
    public class DeleteEnrollmentSubmission
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
                var submission = await _context.Submissions.FindAsync(request.Id);
                if (submission == null)
                {
                    return Result<Unit>.Failure("Not found");
                }
                
                _context.Submissions.Remove(submission);
                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
