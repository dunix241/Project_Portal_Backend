using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Enrollments.Submissions.Theses
{
    public class PublishThesis
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
                var submission = await _context.Submissions
                        .Include(entity => entity.Enrollment)
                        .Where(entity => entity.Id == request.Id && entity.Status == SubmissionStatus.ACCEPTED)
                        .FirstOrDefaultAsync()
                    ;
                if (submission == null)
                {
                    return Result<Unit>.Failure("This project cannot be published");
                }

                submission.Enrollment.IsPublished = true;
                submission.Enrollment.ThesisId = submission.ThesisId;
                _context.Update(submission);
                
                var succeeded = (await _context.SaveChangesAsync()) != 0;

                return succeeded ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Error saving your changes");
            }
        }
    }
}