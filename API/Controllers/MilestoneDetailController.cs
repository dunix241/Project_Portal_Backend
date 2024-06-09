using Application.Core;
using Application.ProjectMilestone.DTOs;
using Application.ProjectMilestoneDetail.DTOs;
using Domain.Project;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    public class MilestoneDetailController : ApiController
    {
        [HttpPost()]
        [SwaggerOperation(Summary = "Add a MilestoneDetail")]
        public async Task<IActionResult> CreateAndAssignToSchool([FromQuery] CreateProjectMilestoneDetailRequest dto)
        {

            var result = await Mediator.Send(new Application.ProjectMilestoneDetail.Create.Command { dto = dto });
            return HandleResult(result);
        }
        [HttpGet]
        [SwaggerOperation(Summary = "List MilestoneDetails")]
        public async Task<IActionResult> ListMilestone([FromQuery] ListProjectMilestoneDetailRequest request, [FromQuery] PagingParams pagination)
        {
            return HandleResult(await Mediator.Send(new Application.ProjectMilestoneDetail.List.Query { pagination = pagination, query = request }));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a MilestoneDetail")]
        public async Task<IActionResult> GetSchool(Guid id)
        {
            return HandleResult(await Mediator.Send(new Application.ProjectMilestoneDetail.Details.Query { Id = id }));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove a MilestoneDetail")]
        public async Task<IActionResult> DeleteLecturer(Guid id)
        {
            return HandleResult(await Mediator.Send((new Application.ProjectMilestoneDetail.Delete.Command { Id = id })));
        }

    }
}
