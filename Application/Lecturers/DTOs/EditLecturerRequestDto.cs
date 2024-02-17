using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lecturers.DTOs
{
    public class EditLecturerRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }  
    }
}
