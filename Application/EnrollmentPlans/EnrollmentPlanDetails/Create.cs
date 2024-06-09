using Application.Core;
using Application.EnrollmentPlans.EnrollmentPlanDetails.DTOs;
using Application.EnrollmentPlans.EnrollmentPlanDetails.ValidationAttributes;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.EnrollmentPlans.EnrollmentPlanDetails;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        [EnrollmentPlanExists]
        public Guid EnrollmentPlanId { get; set; }
        public CreateEnrollmentPlanDetailsRequestDto Payload { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var enrollmentPlanDetails = _mapper.Map<Domain.EnrollmentPlan.EnrollmentPlanDetails>(request.Payload);
            enrollmentPlanDetails.EnrollmentPlanId = request.EnrollmentPlanId;

            _dataContext.Add(enrollmentPlanDetails);

            return await _dataContext.SaveChangesAsync() != 0 ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("A problem occurred while saving Enrollment Plan Details");
        }
    }
    
}