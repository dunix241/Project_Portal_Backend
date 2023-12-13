using Application.Core;
using Domain.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.DTOs
{
    public class ListStudentResponseDto: PagedList<GetStudentResponseDto>
    {

    }
}
