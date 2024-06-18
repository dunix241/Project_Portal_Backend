using Application.Enrollments;
using Application.Enrollments.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Application.Core;

namespace API.Controllers.CMS
{
    public class EnrollmentsController : CmsApiController
    {
        [HttpGet("Semester")]
        [SwaggerOperation(Summary = "List Enrollment by filter (inclue semesterId)")]
        public async Task<ActionResult<ListEnrollmentResponseDto>> ListEnrollmentSemesters(Guid? semesterId, [FromQuery] PagingParams pagingParams)
        {
            return HandleResult(await Mediator.Send(new List.Query { SemesterId = semesterId, PagingParams = pagingParams }));
        }

        [HttpGet("{id}/Members")]
        [SwaggerOperation(Summary = "List all Enrollment members")]
        public async Task<ActionResult<ListEnrollmentMembersResponseDto>> ListEnrollmentMembers(Guid id)
        {
            return HandleResult(await Mediator.Send(new ListEnrollmentMembers.Query { Id = id }));
        }
    }
}
