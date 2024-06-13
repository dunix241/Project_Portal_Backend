using System.Reflection.Metadata;
using Application.Core;
using Application.EnrollmentPlans.DTOs;
using Application.EnrollmentPlans.EnrollmentPlanDetails.DTOs;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.EnrollmentPlans.EnrollmentPlanDetails;

public class List
{
    public class Query : IRequest<Result<ListEnrollmentPlanDetailsResponseDto>>
    {
        public Guid EnrollmentPlanId { get; set; }
        public PagingParams PagingParams { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<ListEnrollmentPlanDetailsResponseDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        
        public async Task<Result<ListEnrollmentPlanDetailsResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _dataContext.EnrollmentPlanDetails
                .Where(entity => entity.EnrollmentPlanId == request.EnrollmentPlanId)
                .Select(entity => _mapper.Map<EnrollmentPlanDetailsResponseDto>(entity))
                .AsQueryable();

            var enrollmentPlanDetailsList = new ListEnrollmentPlanDetailsResponseDto();
            await enrollmentPlanDetailsList.GetItemsAsync(query, request.PagingParams.PageNumber, request.PagingParams.PageSize);

            return Result<ListEnrollmentPlanDetailsResponseDto>.Success(enrollmentPlanDetailsList);
        }
    }
}