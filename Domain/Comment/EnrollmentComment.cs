namespace Domain.Comment
{
    public class EnrollmentComment : CommentBase
    {
        public Guid EnrollmentId { get; set; }
        public Enrollment.Enrollment Enrollment { get; set; }
    }
}
