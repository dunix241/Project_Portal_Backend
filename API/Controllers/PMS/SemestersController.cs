using Application.Core;
using Application.Semesters;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PMS;

public class SemestersController : PmsApiController
{


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
    }

    [HttpGet("{id}/Projects")]
    public async Task<IActionResult> ListProjects(Guid id, [FromQuery] PagingParams pagingParams)
    {
        return HandleResult(await Mediator.Send(new Application.Semesters.Projects.List.Query{SemesterId = id, Params = pagingParams}));
    }
}