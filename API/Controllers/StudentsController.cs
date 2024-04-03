using Application.Core;
using Application.Students;
using Application.Students.DTOs;
using Domain.Lecturer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    public class StudentsController : ApiController
    {
        [HttpGet]
        [SwaggerOperation(Summary = "List students")]
        public async Task<IActionResult> ListStudents([FromQuery] PagingParams pagingParams)
        {
            return HandleResult(await Mediator.Send(new List.Query { QueryParams = pagingParams }));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a student")]
        public async Task<IActionResult> GetStudent(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }
        
        // [HttpGet("BySchoolId")]
        // public async Task<IActionResult> GetStudentBySchoolId(Guid id, [FromQuery] PagingParams pagingParams)
        // {
        //     return HandleResult(await Mediator.Send(new ListStudentBySchool.Query { Id = id, QueryParams = pagingParams }));
        // }
    }
}

