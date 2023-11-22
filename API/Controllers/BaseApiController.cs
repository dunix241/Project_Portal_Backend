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
        if (result == null || result.Value == null) return NotFound();

        if (!result.IsSuccess) return BadRequest(result.Error);

        return Ok(result.Value);
    }

}