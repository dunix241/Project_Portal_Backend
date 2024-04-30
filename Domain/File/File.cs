using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.File
{
    public class File
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileOriginalName { get; set; }
        public FileType FileType { get; set; }

        public Guid? StudentId { get; set; }
        public Student.Student? Student { get; set; }

        public Guid? LecturerId { get; set; }
        public Lecturer.Lecturer? Lecturer { get; set; }

        public Guid? ProjectId { get; set; }
        public Project.Project? Project { get; set; }
    }
    public enum FileType
    {
        Avatar,
        Img,
        Word,
        PDf
    }

}
