using System.ComponentModel.DataAnnotations;

namespace Domain.Student
{
    public class Student
    {
        [Key]
        public string UserId { get; set; }
        public User User { get; set; }
        public long IRN { get; set; }
        public Guid SchoolId { get; set; }
        public School.School School { get; set; }
    }
}
