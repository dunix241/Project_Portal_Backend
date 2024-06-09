using Application.Core;
using Application.ProjectEnrollmentMember.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace API.Controllers.CMS
{
    public class EnrollmentController : CmsApiController
    {

        [HttpGet("Enrollments/cms/:id/Members")]
        [SwaggerOperation(Summary = "List all enrollment memberss")]
        public async Task<IActionResult> List([FromQuery] LidtProjectErollmentMemberRequestDto dto , [FromQuery] PagingParams pagination)
        {
            return HandleResult(await Mediator.Send(new Application.ProjectEnrollmentMember.List.Query { Filter = dto, Pagination = pagination}));
        }

    }
}
