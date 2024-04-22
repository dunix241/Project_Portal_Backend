using Domain.Lecturer;

namespace Application.Lecturers.DTOs
{
    public class GetLecturerResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCurrentMilestoneId { get; set; }

        public static GetLecturerResponseDto FromLecturer (Lecturer lecturer)
        {
            if (lecturer == null)
            {
                return null;
            }

            return new GetLecturerResponseDto
            {
                Id = lecturer.Id,
                Name = lecturer.Name,
                IsActive = lecturer.IsActive,
                SchoolId = lecturer.SchoolId,
                SchoolName = lecturer.School?.Name,
                SchoolCurrentMilestoneId = lecturer.School?.CurrentMilestoneId,
            };
        }
    }
}
