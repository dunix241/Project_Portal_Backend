using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected ActionResult HandleResult<T>(Result<T>? result)
    {
       

        if (result?.Status == Status.BadRequest) return BadRequest(result.Error);
        if (result?.Status == Status.Unauthorized) return Unauthorized(result.Error);
        if (result?.Status == Status.Forbid) return Forbid();

        if (result == null || result.Value == null) return NotFound();

        return Ok(result.Value);
    }

}