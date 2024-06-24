using Application.Core;
using Application.Enrollments.Submissions.DTOs;
using AutoMapper;
using Domain.Submission;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Enrollments.Submissions
{
    public class ListEnrollmentSubmission
    {
        public class Query : IRequest<Result<List<Submission>>>
        {
            public Guid EnrollmentId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<Submission>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<Submission>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Submissions
                    .Include(entity => entity.Thesis)
                    .Where(x => x.EnrollmentId == request.EnrollmentId)
                    .OrderByDescending(x => x.DueDate)
                    .ToList();

                //var submission = new ListSubmissionResponseDto();

                return Result<List<Submission>>.Success(query);
            }
        }
    }
}