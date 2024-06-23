using Application.Students.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Application.Students;
using Application.Students.DTOs;

namespace API.Controllers.PMS
{
    public class StudentsController : PmsApiController
    {
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Edit a student")]
        public async Task<IActionResult> EditStudent(string id, EditStudentRequestDto student)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { Id = id, Student = student }));
        }
    }
}
