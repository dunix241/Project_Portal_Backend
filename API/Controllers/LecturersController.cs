using Application.Core;
using Application.Lecturers;
using Application.Lecturers.DTOs;
using Application.Students.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LecturersController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetLecturers([FromQuery] PagingParams pagingParams)
        {
            return HandleResult(await Mediator.Send(new List.Query { QueryParams = pagingParams }));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetLecturer(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpGet("BySchoolId")]
        public async Task<IActionResult> GetLecturerBySchoolId(Guid schoolID, [FromQuery] PagingParams pagingParams)
        {
            return HandleResult(await Mediator.Send(new ListLecturerOfSchool.Query { Id = schoolID, QueryParams = pagingParams }));
        }

        [HttpPost("LecturerCreateAndAssign")]
        public async Task<IActionResult> Create(CreateLecturerRequedtDto lecturer)
        {
            try
            {
                var result = await Mediator.Send(new Create.Command { Lecturer = lecturer });

                if (result.IsSuccess)
                {
                    return Ok("Lecturer created and assigned to school successfully.");
                }

                return BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(400, "An error occurred while processing your request." + "\n" + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditLecturer(Guid id, EditLecturerRequestDto lecturer)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { Id = id, Lecturer = lecturer }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecturer(Guid id)
        {
            return HandleResult(await Mediator.Send((new Delete.Command { Id = id })));
        }
    }
}
