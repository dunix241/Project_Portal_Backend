using Application.Enrollments;
using Application.Enrollments.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.Showcase
{
    public class EnrollmentController : ShowcaseApiController
    {
        [HttpGet("GetAll")]
        [SwaggerOperation(Summary = "List Enrollment by filter (inclue semesterId,schoolId, userId,isPublished)")]
        public async Task<ActionResult<ListEnrollmentResponseDto>> ListEnrollmentSemesters([FromQuery] ListEnrollmentRequestDto dto)
        {
            return HandleResult(await Mediator.Send(new ListEnrollmentWithThesis.Query { Payload = dto }));
        }
    }
}
