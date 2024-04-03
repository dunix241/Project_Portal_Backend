using Application.Lecturers;
using Application.Lecturers.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.CMS;

public class LecturersController : CmsApiController
{
        [HttpPost]
        [SwaggerOperation(Summary = "Add a lecturer")]
        public async Task<IActionResult> CreateLecturer(CreateLecturerRequedtDto lecturer)
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
        [SwaggerOperation(Summary = "Update a lecturer's information")]
        public async Task<IActionResult> EditLecturer(Guid id, EditLecturerRequestDto lecturer)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { Id = id, Lecturer = lecturer }));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove a lecturer")]
        public async Task<IActionResult> DeleteLecturer(Guid id)
        {
            return HandleResult(await Mediator.Send((new Delete.Command { Id = id })));
        }
}