using Application.Core;
using Application.Enrollments.Submissions.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Enrollments.Submissions
{
    public class DetailEnrollmentSubmission
    {
        public class Query : IRequest<Result<DTOs.GetSubmissionResponseDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DTOs.GetSubmissionResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<DTOs.GetSubmissionResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var submission = await _context.Submissions
                    .Where(s => s.Id == request.Id)
                    .Include(s => s.Enrollment)
                        .ThenInclude(e => e.ProjectSemester)
                            .ThenInclude(ps => ps.Project)
                    .Include(s => s.SubmissionComments)
                    .Select(x => new DTOs.GetSubmissionResponseDto
                    {
                        DueDate = x.DueDate,
                        EnrollmentId = x.EnrollmentId,
                        Id = x.Id,
                        Status = x.Status,
                        SubmittedDate = x.SubmittedDate,
                        ThesisId = x.ThesisId,
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (submission == null)
                {
                    return null;
                }

                var responseDto = _mapper.Map<DTOs.GetSubmissionResponseDto>(submission);

                return Result<DTOs.GetSubmissionResponseDto>.Success(responseDto);
            }
        }
    }
}
