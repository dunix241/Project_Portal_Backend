namespace Domain.Comment
{
    public class CommentBase
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
    }
}
