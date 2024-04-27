namespace Domain.Lecturer
{
    public class Lecturer: Person.Person
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid SchoolId { get; set; }
        public School.School School { get; set; }
    }
}
