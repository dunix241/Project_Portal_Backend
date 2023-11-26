using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Student;

namespace Domain.School
{
    public class School
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CurrentMilestoneId { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Student.Student> Studens { get; set; }
        public ICollection<Lecturer.Lecturer> Lecturers { get; set; }

    }
}
