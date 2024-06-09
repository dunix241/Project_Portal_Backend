using Application.Core;
using Application.EnrollmentPlans.DTOs;
using AutoMapper;
using Domain.EnrollmentPlan;
using MediatR;
using Persistence;

namespace Application.EnrollmentPlans;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public CreateEnrollmentPlanRequestDto Payload { get; set; }
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
            var enrollmentPlan = _mapper.Map<EnrollmentPlan>(request.Payload);
            _dataContext.Add(enrollmentPlan);
            await _dataContext.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
    }
}