using Application.Core;
using Application.ProjectMilestone.DTOs;
using Application.ProjectMilestoneDetail.DTOs;
using Application.Students.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using ProjectMilestoneDetail = Application.ProjectMilestoneDetail;
using ProjectMilestone = Application.ProjectMilestone;

namespace API.Controllers
{
    public class MilestoneController : ApiController
    {
        [HttpGet]
        [SwaggerOperation(Summary = "List milestone details")]
        public async Task<IActionResult> ListMilestone( [FromQuery] ListProjectMilestoneDetailRequest request, [FromQuery] PagingParams pagination)
        {
            return HandleResult(await Mediator.Send(new ProjectMilestoneDetail.List.Query {pagination = pagination, query = request }));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a milestone detail")]
        public async Task<IActionResult> GetSchool(Guid id)
        {
            return HandleResult(await Mediator.Send(new ProjectMilestoneDetail.Details.Query {  Id = id }));
        }

        [HttpPost()]
        [SwaggerOperation(Summary = "Add a milestone")]
        public async Task<IActionResult> CreateAndAssignToSchool([FromQuery]CreateProjectMilestoneRequestDto dto)
        {

            var result = await Mediator.Send(new ProjectMilestone.Create.Command { dto = dto});
            return HandleResult(result);
        }

    }
}
