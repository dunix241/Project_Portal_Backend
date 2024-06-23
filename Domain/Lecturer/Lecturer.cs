using System.ComponentModel.DataAnnotations;

namespace Domain.Lecturer
{
    public class Lecturer
    {
        [Key]
        public string UserId { get; set; }
        public User User { get; set; }
        public string? Title { get; set; }
        public string? Headline { get; set; }
        public string? Description { get; set; }
        public Guid SchoolId { get; set; }
        public School.School School { get; set; }
        public bool ContactViaPhoneNumber { get; set; } = true;
        public bool ContactViaEmail { get; set; } = true;
    }
}
