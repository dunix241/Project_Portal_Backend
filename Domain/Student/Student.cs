using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.School.School;
namespace Domain.Student
{
    public class Student : Person.Person
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public Guid SchoolId { get; set; }
        public  School { get; set; }
    }
}
