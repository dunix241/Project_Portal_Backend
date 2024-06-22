
namespace Application.Enrollments.DTOs
{
    public class GetEnrollmentHistoryResponseDto
    {
        public Guid SemesterId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<EnrollmentDto> Enrollments { get; set; }
    }

    public class EnrollmentDto
    {
        public Guid EnrollmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime RegisterDate { get; set; }
        public List<SubmissionDto> Submissions { get; set; }
    }
}
