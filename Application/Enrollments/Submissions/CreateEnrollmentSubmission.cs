using Application.Core;
using Application.Enrollments.Submissions.DTOs;
using AutoMapper;
using Domain.Submission;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Enrollments.Submissions
{
    public class CreateEnrollmentSubmission
    {
        public class Command : IRequest<Result<Submission>>
        {
            public CreateSubmissionRequestDto Submission { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Submission>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Submission>> Handle(Command request, CancellationToken cancellationToken)
            {
                var submission = new Submission();
                var requestSubmission = request.Submission;

                if (requestSubmission.DueDate <= DateTime.Now)
                {
                    return Result<Submission>.Failure("Invalid duedate");
                }

                var enrollment = await _context.Enrollments.FindAsync(requestSubmission.EnrollmentId);
                enrollment.SemesterId = (await _context.Semesters.FirstOrDefaultAsync
                                        (entity => entity.StartRegistrationDate <= DateTime.Today && entity.EndRegistrationDate >= DateTime.Today))!.Id;
                if (enrollment.SemesterId == null)
                {
                    return Result<Submission>.Failure("Ouside the allowed time");
                }

                if (enrollment.IsPublished)
                {
                    return Result<Submission>.Failure("Ouside the allowed time. Can not put comment to published enrollment");
                }

                var acceptedSubmission = await _context.Submissions
                .Where(x => (x.EnrollmentId == requestSubmission.EnrollmentId)
                && (x.Status == SubmissionStatus.ACCEPTED)).FirstOrDefaultAsync();

                if (acceptedSubmission != null)
                {
                    return Result<Submission>.Failure("You can only add new submission when there is no accpeted submission");
                }

                _mapper.Map(request.Submission, submission);

                _context.Submissions.Add(submission);
                await _context.SaveChangesAsync();

                return Result<Submission>.Success(submission);
            }
        }
    }
}
