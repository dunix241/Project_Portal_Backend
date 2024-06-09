using Application.Core;
using MediatR;
using Persistence;

namespace Application.EnrollmentPlans;

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
            var enrollmentPlan = await _dataContext.EnrollmentPlans.FindAsync(request.Id);
            if (enrollmentPlan == null) return null;

            _dataContext.EnrollmentPlans.Remove(enrollmentPlan);
            await _dataContext.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
    
}