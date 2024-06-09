using Application.Core;
using Application.Lecturers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    public class LecturersController : ApiController
    {
        [HttpGet]
        [SwaggerOperation(Summary = "List lecturers")]
        public async Task<IActionResult> ListLectures([FromQuery] PagingParams pagingParams)
        {
            return HandleResult(await Mediator.Send(new List.Query { QueryParams = pagingParams }));
        }


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a lecturer")]
        public async Task<IActionResult> GetLecturer(string id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        // [HttpGet("BySchoolId")]
        // public async Task<IActionResult> GetLecturerBySchoolId(Guid schoolID, [FromQuery] PagingParams pagingParams)
        // {
        //     return HandleResult(await Mediator.Send(new ListLecturerOfSchool.Query { Id = schoolID, QueryParams = pagingParams }));
        // }
    }
}
