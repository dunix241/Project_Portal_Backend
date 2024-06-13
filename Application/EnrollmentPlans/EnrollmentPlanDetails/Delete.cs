using Application.Core;
using MediatR;
using Persistence;

namespace Application.EnrollmentPlans.EnrollmentPlanDetails;

public class Delete
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
            var enrollmentPlanDetails = await _dataContext.EnrollmentPlanDetails.FindAsync(request.Id);
            
            if (enrollmentPlanDetails == null) return null;

            _dataContext.Remove(enrollmentPlanDetails);
            return await _dataContext.SaveChangesAsync() != 0
                ? Result<Unit>.Success(Unit.Value)
                : Result<Unit>.Failure("A problem occurred while deleting Enrollment Plan Details");
        }
    }
}