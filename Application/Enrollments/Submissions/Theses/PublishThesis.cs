using Application.Core;
using AutoMapper;
using Domain.Submission;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace Application.Enrollments.Submissions.Theses
{
    public class PublishThesis
    {
        public class Command : IRequest<Result<Domain.Enrollment.Enrollment>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Domain.Enrollment.Enrollment>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Domain.Enrollment.Enrollment>> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentSubmission = await _context.Submissions.FindAsync(request.Id);
                if (currentSubmission == null)
                {
                    return null;
                }


                if (currentSubmission.Status != SubmissionStatus.COMPLETED )
                {
                    return Result<Domain.Enrollment.Enrollment>.Failure("You can only publish one thesis from  a completed submission");
                }           

                var enrollment = await _context.Enrollments.FindAsync(currentSubmission.EnrollmentId);
                enrollment.IsPublished = true;
                enrollment.ThesisId = currentSubmission.ThesisId;
                enrollment.PublishDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Result<Domain.Enrollment.Enrollment>.Success(enrollment);
            }
        }
    }
}
