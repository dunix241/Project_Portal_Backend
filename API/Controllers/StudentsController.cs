using Application.Core;
using Application.Students;
using Application.Students.DTOs;
using Domain.Lecturer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StudentsController : ApiController
    {

        [HttpGet]
        public async Task<IActionResult> GetStudents([FromQuery] PagingParams pagingParams)
        {
            return HandleResult(await Mediator.Send(new List.Query { QueryParams = pagingParams }));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }
        [HttpGet("BySchoolId")]
        public async Task<IActionResult> GetStudentBySchoolId(Guid id, [FromQuery] PagingParams pagingParams)
        {
            return HandleResult(await Mediator.Send(new ListStudentBySchool.Query { Id = id, QueryParams = pagingParams }));
        }
        [HttpPost("CreateAndAssign")]
        public async Task<IActionResult> CreateAndAssignToSchool(CreateStudentRequestDto student)
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
        public async Task<IActionResult> EditStudent(Guid id, EditStudentRequestDto school)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { Id = id, Student = school }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            return HandleResult(await Mediator.Send((new Delete.Command { Id = id })));
        }
    }
}

