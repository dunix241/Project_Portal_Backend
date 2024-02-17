using Application.Core;
using Application.Projects;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PMS;

public class ProjectsController : PmsApiController
{
    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PagingParams pagingParams)
    {
        return HandleResult(await Mediator.Send(new List.Query{QueryParams = pagingParams}));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
    }
}