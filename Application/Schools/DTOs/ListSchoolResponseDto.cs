using Application.Core;
using Domain.MockDomain;
using Domain.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Schools.DTOs
{
    public class ListSchoolResponseDto : PagedList<School>
    {
    }
}
