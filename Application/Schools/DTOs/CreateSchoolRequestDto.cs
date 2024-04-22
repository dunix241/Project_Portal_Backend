namespace Application.Schools.DTOs
{
    public class CreateSchoolRequestDto
    {
        public string Name { get; set; }
        public string CurrentMilestoneId { get; set; }
        public bool IsActive { get; set; }
    }
}
