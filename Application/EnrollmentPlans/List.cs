using Application.Core;
using Application.EnrollmentPlans.DTOs;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.EnrollmentPlans;

public class List
{
    public class Query : IRequest<Result<ListEnrollmentPlansResponseDto>>
    {
        public PagingParams PagingParams { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<ListEnrollmentPlansResponseDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<Result<ListEnrollmentPlansResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _dataContext.EnrollmentPlans
                .Select(entity => _mapper.Map<EnrollmentPlanResponseDto>(entity))
                .AsQueryable();

            var enrollmentPlans = new ListEnrollmentPlansResponseDto();
            await enrollmentPlans.GetItemsAsync(query, request.PagingParams.PageNumber, request.PagingParams.PageSize);

            return Result<ListEnrollmentPlansResponseDto>.Success(enrollmentPlans);
        }
    }
}