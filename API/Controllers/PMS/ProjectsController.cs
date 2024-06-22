using Application.Core;
using Application.Enrollments;
using Application.Enrollments.DTOs;
using Application.Projects;
using Application.Projects.DTOs;
using Domain.Project;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PMS;

public class ProjectsController : PmsApiController
{
    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PagingParams pagingParams)
    {
        return HandleResult(await Mediator.Send(new Application.Projects.List.Query{QueryParams = pagingParams}));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
    }

    [HttpGet("Overview")]
    public async Task<ActionResult<ListBasedOnEnrollmentPlanResponseDto>> GetProjectOverview()
    {
        return HandleResult(await Mediator.Send(new ListBasedOnEnrollmentPlan.Query()));
    }
}