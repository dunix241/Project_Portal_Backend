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
        public ListEnrollmentRequestDto Payload;
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
            var payload = request.Payload;
            var query = _dataContext.Enrollments
      .Where(x =>
          (payload.SemesterId == null || x.SemesterId == payload.SemesterId) &&
          (payload.UserId == null || x.OwnerId == payload.UserId) &&
          (payload.IsPublished == null || x.IsPublished == payload.IsPublished) &&
          (payload.SchoolId == null || payload.SchoolId == x.ProjectSemester.Project.SchoolId)
      );


            var enrollmentPlans = new ListEnrollmentResponseDto();
            await enrollmentPlans.GetItemsAsync(query, payload.PageNumber, payload.PageSize);

            return Result<ListEnrollmentResponseDto>.Success(enrollmentPlans);
        }
    }
}

