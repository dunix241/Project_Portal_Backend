using Application.Core;
using Application.Semesters;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PMS;

public class SemestersController : PmsApiController
{
    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PagingParams pagingParams)
    {
        return HandleResult(await Mediator.Send(new List.Query{Params = pagingParams}));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
    }

    [HttpGet("{id}/Projects")]
    public async Task<IActionResult> ListProjects([FromQuery] PagingParams pagingParams)
    {
        return HandleResult(await Mediator.Send(new Application.Semesters.Projects.List.Query{Params = pagingParams}));
    }
}