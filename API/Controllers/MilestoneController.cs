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

        [HttpPost()]
        [SwaggerOperation(Summary = "Add a milestone")]
        public async Task<IActionResult> CreateAndAssignToSchool([FromQuery] CreateProjectMilestoneRequestDto dto)
        {

            var result = await Mediator.Send(new ProjectMilestone.Create.Command { dto = dto });
            return HandleResult(result);
        }

        [HttpGet("List")]
        [SwaggerOperation(Summary = "List milestone ")]
        public async Task<IActionResult> ListMilestone([FromQuery] ListProjectMilestoneRequestDto request, [FromQuery] PagingParams pagination)
        {
            return HandleResult(await Mediator.Send(new ProjectMilestone.List.Query { pagination = pagination, query = request }));
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get by id ")]
        public async Task<IActionResult> GetMilestone([FromQuery] Guid id)
        {
            return HandleResult(await Mediator.Send(new ProjectMilestone.Detail.Query {  Id = id }));
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove a milestone")]
        public async Task<IActionResult> DeleteLecturer(Guid id)
        {
            return HandleResult(await Mediator.Send((new ProjectMilestone.Delete.Command { Id = id })));
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Edit a milestone")]
        public async Task<IActionResult> EditStudent(Guid id, EditProjectMilestoneRequestDto dto)
        {
            return HandleResult(await Mediator.Send(new ProjectMilestone.Edit.Command { Id = id, dto = dto }));
        }
    }
}
