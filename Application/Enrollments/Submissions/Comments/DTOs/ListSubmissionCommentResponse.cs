using Application.Core;
using Domain.Comment;

namespace Application.Enrollments.Submissions.Comments.DTOs
{
    public class ListSubmissionCommentResponse : PagedList<SubmissionCommentDto>
    {

    }
    public class SubmissionCommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
