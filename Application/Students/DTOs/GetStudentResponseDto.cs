using Domain.Student;
using System;

namespace Application.Students.DTOs
{
    public class GetStudentResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCurrentMilestoneId { get; set; }

        public static GetStudentResponseDto FromStudent(Student student)
        {
            if (student == null)
            {
                return null;
            }

            return new GetStudentResponseDto
            {
                Id = student.Id,
                Name = student.Name,
                IsActive = student.IsActive,
                SchoolId = student.SchoolId,
                SchoolName = student.School?.Name,
                SchoolCurrentMilestoneId = student.School?.CurrentMilestoneId,
            };
        }
    }
}
