using Application.Core;
using Application.EnrollmentPlans.DTOs;
using Application.Enrollments.DTOs;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Enrollments;

public class List
{
    public class Query : IRequest<Result<ListEnrollmentResponseDto>>
    {
        public Guid? SemesterId { get; set; }
        public PagingParams PagingParams { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<ListEnrollmentResponseDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<Result<ListEnrollmentResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _dataContext.Enrollments.Where(x => ( request.SemesterId == null || x.SemesterId == request.SemesterId));
            var enrollmentPlans = new ListEnrollmentResponseDto();
            await enrollmentPlans.GetItemsAsync(query, request.PagingParams.PageNumber, request.PagingParams.PageSize);

            return Result<ListEnrollmentResponseDto>.Success(enrollmentPlans);
        }
    }
}

