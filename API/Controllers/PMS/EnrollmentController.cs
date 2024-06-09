using Application.Core;
using Application.ProjectEnrollment.DTOs;
using Application.ProjectMilestone.DTOs;
using Domain.Project;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.PMS
{
    public class EnrollmentController : PmsApiController
    {

        [HttpPost()]
        [SwaggerOperation(Summary = "Add an enrollment")]
        public async Task<IActionResult> CreateAndAssignToSchool([FromQuery] CreateProjectEnrollmentRequestDto dto)
        {
            var result = await Mediator.Send(new Application.ProjectEnrollment.Create.Command { dto = dto });
            return HandleResult(result);
        }
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Edit an enrollment")]
        public async Task<IActionResult> Edit(Guid id, EditProjectEnrollmentRequestDto project)
        {
            return HandleResult(await Mediator.Send(new Application.ProjectEnrollment.Edit.Command { Id = id, dto = project }));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an enrollment")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await Mediator.Send((new Application.ProjectEnrollment.Delete.Command { Id = id })));
        }
        [HttpGet]
        public async Task<IActionResult> List([FromQuery]ListProjectEnrollmentRequestDto dto, [FromQuery] PagingParams pagination)
        {
            return HandleResult(await Mediator.Send(new  Application.ProjectEnrollment.List.Query { query = dto, pagination = pagination }));
        }

    }
}
