using Application.Core;
using Application.Enrollments.Submissions.Comments.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Enrollments.Submissions.Comments
{
    public class ListSubmissionComment
    {
        public class Query : IRequest<Result<ListSubmissionCommentResponse>>
        {
            public Guid SubmissionId { get; set; }
            public PagingParams PagingParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ListSubmissionCommentResponse>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;

            public Handler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<Result<ListSubmissionCommentResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _dataContext.SubmissionComments
                     .Where(c => c.SubmissionId == request.SubmissionId)
                     .Include(c => c.User)
                     .Select(c => new SubmissionCommentDto
                     {
                         Id = c.Id,
                         Content = c.Content,
                         Date = c.Date,
                         UserId = c.UserId,
                         UserName = c.User.UserName
                     });

                var comments = new ListSubmissionCommentResponse();
                await comments.GetItemsAsync(query, request.PagingParams.PageNumber, request.PagingParams.PageSize);

                return Result<ListSubmissionCommentResponse>.Success(comments);
            }
        }
    }
}

