namespace Application.Enrollments.DTOs
{
    public class GetEnrollmentDetailResponseDto
    {
        public Guid EnrollmentId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime RegisterDate { get; set; }
        public ProjectDto Project { get; set; }
        public SemesterDto Semester { get; set; }
        public List<EnrollmentMemberDto> Members { get; set; }
        public List<SubmissionDto> Submissions { get; set; }
    }
    public class SubmissionDto
    {
        public Guid Id { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public Guid? ThesisId { get; set; }
    }

    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SchoolName { get; set; }
    }

    public class SemesterDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
