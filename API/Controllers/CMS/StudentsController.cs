using Application.Students;
using Application.Students.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.CMS;

public class StudentsController : CmsApiController
{
        [HttpPost]
        [SwaggerOperation(Summary = "Add a student")]
        public async Task<IActionResult> CreateStudent(CreateStudentRequestDto student)
        {
            try
            {
                var result = await Mediator.Send(new Create.Command { Student = student });

                if (result.IsSuccess)
                {
                    return Ok("Student created and assigned to school successfully.");
                }

                return BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(400, "An error occurred while processing your request." + "\n" + ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a student's information")]
        public async Task<IActionResult> EditStudent(string id, EditStudentRequestDto school)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { Id = id, Student = school }));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove a student")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            return HandleResult(await Mediator.Send((new Delete.Command { Id = id })));
        }
}