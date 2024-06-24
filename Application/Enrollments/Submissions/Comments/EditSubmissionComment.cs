using Application.Core;
using Application.Enrollments.Submissions.Comments.DTOs;
using Application.Enrollments.Submissions.DTOs;
using AutoMapper;
using Domain.Comment;
using Domain.Submission;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enrollments.Submissions.Comments
{
    public class EditSubmissionComment
    {
        public class Command : IRequest<Result<SubmissionComment>>
        {
            public Guid Id { get; set; }
            public EditSubmissionCommentRequest Dto  { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<SubmissionComment>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<SubmissionComment>> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentSubmission = await _context.SubmissionComments.FindAsync(request.Id);
                if (currentSubmission == null)
                {
                    return null;
                }
              
                _mapper.Map(request.Dto, currentSubmission);

                await _context.SaveChangesAsync();

                return Result<SubmissionComment>.Success(currentSubmission);
            }
        }
    }
}
