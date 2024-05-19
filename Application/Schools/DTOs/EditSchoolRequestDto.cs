namespace Application.Schools.DTOs
{
    public class EditSchoolRequestDto
    {
        public string Name { get; set; }
        public string? CurrentMilestoneId { get; set; }
        public bool IsActive { get; set; }
    }
}
