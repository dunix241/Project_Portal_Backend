using Application.Lecturers;
using Application.Lecturers.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.PMS
{
    public class LecturersController : PmsApiController
    {
        [HttpGet]
        [SwaggerOperation(Summary = "Get Lecturer Profile")]
        public async Task<IActionResult> CreateSchool(string id)
        {
            return HandleResult(await Mediator.Send(new CMSDetail.Query { Id = id }));
        }
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a lecturer's information")]
        public async Task<IActionResult> EditLecturer(string id, EditLecturerRequestDto lecturer)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { Id = id, Lecturer = lecturer }));
        }
    }
}
