using Application.Core;
using Application.Enrollments.Submissions.Comments.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Comment;
using Domain.School;
using MediatR;
using Persistence;

namespace Application.Enrollments.Submissions.Comments
{
    public class CreateSubmissionComment
    {
        public class Command : IRequest<Result<SubmissionComment>>
        {
            public CreateSubmissionCommentRequest Payload { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<SubmissionComment>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _context = context;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

            public async Task<Result<SubmissionComment>> Handle(Command request, CancellationToken cancellationToken)
            {
                
                var requestDto = request.Payload;
                var userId = _userAccessor.GetUser().Id;

                if (userId == null)
                {
                    return Result<SubmissionComment>.Failure(Status.Forbid, "Unauthorized");
                }
                var comment = new SubmissionComment
                {
                    SubmissionId = requestDto.SubmissionId,
                    Content = requestDto.Content,
                    Date = requestDto.Date,
                    UserId = userId,
                };

                _context.SubmissionComments.Add(comment);
                await _context.SaveChangesAsync();

                return Result<SubmissionComment>.Success(comment);
            }
        }
    }
}
