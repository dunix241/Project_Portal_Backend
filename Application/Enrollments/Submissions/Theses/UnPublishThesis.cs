

using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Enrollments.Submissions.Theses
{
    public class UnPublishThesis
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

                var enrollment = await _context.Enrollments.FindAsync(currentSubmission.EnrollmentId);
                if (enrollment == null)
                {
                    return null;
                }
                if(enrollment.IsPublished == false)
                {
                    return Result<Domain.Enrollment.Enrollment>.Failure("Canot unpiblished a thesis that is not publish yet");
                }
                enrollment.IsPublished = false;
                enrollment.ThesisId = null;
                enrollment.PublishDate = null;               

                await _context.SaveChangesAsync();

                return Result<Domain.Enrollment.Enrollment>.Success(enrollment);
            }
        }
    }
}
