using Application.Core;
using Application.EnrollmentPlans.DTOs;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.EnrollmentPlans;

public class Edit
{
    public class Command : IRequest<Result<Domain.EnrollmentPlan.EnrollmentPlan>>
    {
        public Guid Id { get; set; }
        public EditEnrollmentPlanRequestDto Payload { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Domain.EnrollmentPlan.EnrollmentPlan>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        
        public async Task<Result<Domain.EnrollmentPlan.EnrollmentPlan>> Handle(Command request, CancellationToken cancellationToken)
        {
            var enrollmentPlan = await _dataContext.EnrollmentPlans.FindAsync(request.Id);
            if (enrollmentPlan == null) return null;
            
            _mapper.Map(request.Payload, enrollmentPlan);

            return Result<Domain.EnrollmentPlan.EnrollmentPlan>.Success(enrollmentPlan);
        }
    }
    
}