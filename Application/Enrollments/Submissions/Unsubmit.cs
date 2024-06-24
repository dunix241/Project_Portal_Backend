using Application.Core;
using MediatR;
using Persistence;

namespace Application.Enrollments.Submissions;

public class UnSubmit
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _dataContext;

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var submission = await _dataContext.Submissions.FindAsync(request.Id);
            if (submission == null) return null;
            
            if (submission.Status != SubmissionStatus.SUBMITTED)
            {
                return Result<Unit>.Failure("Cannot remove");
            }

            submission.Status = null;
            submission.SubmittedDate = null;
            submission.ThesisId = null;

            var succeeded = (await _dataContext.SaveChangesAsync()) != 0;

            return succeeded ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Error saving your changes");
        }
    }
}