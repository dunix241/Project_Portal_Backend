using Application.Core;
using AutoMapper;
using Domain.Submission;
using MediatR;
using Persistence;

namespace Application.Enrollments.Submissions
{
    public class UnsubmitThesis
    {
        public class Command : IRequest<Result<Submission>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Submission>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Submission>> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentSubmission = await _context.Submissions.FindAsync(request.Id);
                if (currentSubmission == null)
                {
                    return null;
                }
                var currentStatus = currentSubmission.Status;

                if (currentStatus == SubmissionStatus.ACCEPTED || currentStatus == SubmissionStatus.COMPLETED)
                {
                    return Result<Submission>.Failure("Can not unsumitted a submission which status is Accepted or Completed");
                }

                currentSubmission.Status = SubmissionStatus.UNSUBMITTED;
                currentSubmission.SubmittedDate = null;
                currentSubmission.ThesisId = null;

                await _context.SaveChangesAsync();

                return Result<Submission>.Success(currentSubmission);
            }
        }
    }
}