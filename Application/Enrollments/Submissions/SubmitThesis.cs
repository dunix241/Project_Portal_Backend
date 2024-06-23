using Application.Core;
using Application.Minio;
using Application.Minio.DTOs;
using AutoMapper;
using Domain.Submission;
using MediatR;
using Persistence;

namespace Application.Enrollments.Submissions
{
    public class SubmitThesis
    {
        public class Command : IRequest<Result<Submission>>
        {
            public Guid Id { get; set; }
            public AddFileRequestDto Dto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Submission>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(DataContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;

            }

            public async Task<Result<Submission>> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentSubmission = await _context.Submissions.FindAsync(request.Id);
                if (currentSubmission == null)
                {
                    return null;
                }

                var payload = request.Dto;
                var thesisResponse  = await _mediator.Send(new UploadFile.Command { Payload = payload });
                if (thesisResponse == null)
                {
                    return Result<Submission>.Failure("Error uploading file");
                }

                if(currentSubmission.Status == SubmissionStatus.COMPLETED || currentSubmission.Status == SubmissionStatus.ACCEPTED)
                {
                    return Result<Submission>.Failure("Complted and Accepted submission status do not allow updating");
                }

                if(currentSubmission.Status == SubmissionStatus.UNSUBMITTED)
                {
                    currentSubmission.Status = SubmissionStatus.SUBMITTED;
                }

                currentSubmission.ThesisId = thesisResponse.Value.Id;  
                currentSubmission.SubmittedDate = DateTime.Now;

                await _context.SaveChangesAsync();

                return Result<Submission>.Success(currentSubmission);
            }
        }
    }
}
