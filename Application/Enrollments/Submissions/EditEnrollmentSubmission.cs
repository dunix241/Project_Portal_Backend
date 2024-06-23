using Application.Core;

using Application.Enrollments.Submissions.DTOs;
using AutoMapper;
using Domain.Submission;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Enrollments.Submissions
{
    public class EditEnrollmentSubmission
    {
        public class Command : IRequest<Result<Submission>>
        {
            public Guid Id { get; set; }
            public EditSubmissionRequestDto Dto { get; set; }
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
                var currentSubmission = await _context.Submissions.FindAsync(request.Id);
                if (currentSubmission == null)
                {
                    return null;
                }

                var newStatus = request.Dto.Status;
                var currentStatuss = currentSubmission.Status;
                var enrollment = await _context.Enrollments.FindAsync(currentSubmission.EnrollmentId);

                if (newStatus != null && newStatus != currentStatuss)
                {
                    if (!SubmissionStatus.StatusList.Contains(newStatus))
                    {
                        return Result<Submission>.Failure("Invalid status");
                    }

                    if (newStatus == SubmissionStatus.ACCEPTED || newStatus == SubmissionStatus.COMPLETED)
                    {
                        enrollment = await _context.Enrollments.FindAsync(currentSubmission.EnrollmentId);
                        enrollment.SemesterId = (await _context.Semesters.FirstOrDefaultAsync
                                                (entity => entity.StartRegistrationDate <= DateTime.Today && entity.EndRegistrationDate >= DateTime.Today))!.Id;
                        if (enrollment.SemesterId == null)
                        {
                            return Result<Submission>.Failure("Outside the allowed time");
                        }

                        if(currentSubmission.ThesisId == null)
                        {
                            return Result<Submission>.Failure("The target status require a submitted thesis");
                        }
                    }
                }

                if(newStatus == SubmissionStatus.UNSUBMITTED)
                {

                    currentSubmission.ThesisId = null;
                    currentSubmission.SubmittedDate = null;
                }
                if(currentStatuss == SubmissionStatus.COMPLETED && enrollment.IsPublished && newStatus != currentStatuss)
                {
                    enrollment.ThesisId = null;
                    enrollment.IsPublished = false;
                    enrollment.PublishDate = null;
                }

                _mapper.Map(request.Dto, currentSubmission);

                await _context.SaveChangesAsync();

                return Result<Submission>.Success(currentSubmission);
            }
        }
    }
}
