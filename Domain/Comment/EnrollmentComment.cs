namespace Domain.Comment
{
    public class EnrollmentComment : Comment
    {
        public Guid EnrollmentId { get; set; }
        public Enrollment.Enrollment Enrollment { get; set; }
    }
}
