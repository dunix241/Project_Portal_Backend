using Application.Core;
using Application.ProjectEnrollment.DTOs;
using Application.ProjectEnrollmentMember.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.PMS
{
    public class EnrollmentMemberController : PmsApiController
    {

        [HttpPost()]
        [SwaggerOperation(Summary = "Add an enrollment member")]
        public async Task<IActionResult> Create([FromQuery] CreateProjectEnrollmentMemberRequest dto)
        {
            var result = await Mediator.Send(new Application.ProjectEnrollmentMember.Create.Command { Dto = dto, });
            return HandleResult(result);
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an enrollment member")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await Mediator.Send((new Application.ProjectEnrollmentMember.Delete.Command { Id = id })));
        }
    }
}
