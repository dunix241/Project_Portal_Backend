using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Student
{
    public class Student : Person.Person
    {
        public Guid Id { get; set; }
        public long IRN { get; set; }
        public bool IsActive { get; set; }
        public Guid SchoolId { get; set; }
        public School.School School { get; set; }
        public IList<File.File> Files { get;}
    }
}
