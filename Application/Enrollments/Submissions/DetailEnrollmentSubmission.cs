using Application.Core;
using Application.Minio;
using Application.Minio.DTOs;
using AutoMapper;
using Domain;
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
            private readonly IMediator _mediator;

            public Handler(DataContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
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
                        FileResponseDto = new Minio.DTOs.FileResponseDto { Id = x.ThesisId }
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (submission == null)
                {
                    return null;
                }

                var fileDto = submission.FileResponseDto;
                var fileId = fileDto?.Id;
                if (fileId != null)
                {
                    var file = await _context.Files.Where(s => s.Id == fileId).FirstOrDefaultAsync();
                    if (file != null)
                    {
                        var response = await _mediator.Send(new GetFile.Query { Id = file.Id });
                        if (response != null)
                        {
                            fileDto = response.Value;
                        }
                    }
                }
                submission.FileResponseDto = fileDto;

                //var responseDto = _mapper.Map<DTOs.GetSubmissionResponseDto>(submission);

                return Result<DTOs.GetSubmissionResponseDto>.Success(submission);
            }
        }
    }
}
