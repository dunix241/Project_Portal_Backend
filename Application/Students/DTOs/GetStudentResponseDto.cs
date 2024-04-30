using Domain.Person;
using Domain.Student;

namespace Application.Students.DTOs
{
    public class GetStudentResponseDto : Person
    {
        public Guid Id { get; set; }
        public long IRN { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; } 
        public Guid SchoolId { get; set; }
        public string? SchoolName { get; set; }

        public string? SchoolCurrentMilestoneId { get; set; }

        public static GetStudentResponseDto FromStudent(Student student)
        {
            if (student == null)
            {
                return null;
            }

            return new GetStudentResponseDto
            {
                Id = student.Id,
                IRN = student.IRN,
                FullName = student.FullName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                IsActive = student.IsActive,
                SchoolId = student.SchoolId,
                SchoolName = student.School?.Name,
                SchoolCurrentMilestoneId = student.School?.CurrentMilestoneId,
            };
        }
    }
}
