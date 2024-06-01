using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Comment
{
    public class CommentBase
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        // Navigation properties
        public Guid? StudentId { get; set; }
        public Guid? LecturerId { get; set; }

        public Student.Student? Student { get; set; }
        public Lecturer.Lecturer? Lecturer { get; set; }
    }
}
