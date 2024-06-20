using Application.Core;
using Application.Enrollments.Submissions.DTOs;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Enrollments.Submissions
{
    public class ListEnrollmentSubmission
    {
        public class Query : IRequest<Result<ListSubmissionResponseDto>>
        {
            public Guid EnrollmentId { get; set; }
            public PagingParams QueryParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ListSubmissionResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<ListSubmissionResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Submissions
                     .Where(x => x.EnrollmentId == request.EnrollmentId)
                     .OrderByDescending(x => x.DueDate)
                     .AsQueryable();

                var submission = new ListSubmissionResponseDto();
                await submission.GetItemsAsync(query, request.QueryParams.PageNumber, request.QueryParams.PageSize);

                return Result<ListSubmissionResponseDto>.Success(submission);
            }
        }
    }
}