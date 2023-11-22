using Application.Core;
using Application.MockDomains;
using Application.MockDomains.DTOs;
using Domain.MockDomain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MockDomainsController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetMockDomains([FromQuery] PagingParams pagingParams)
    {
        return HandleResult(await Mediator.Send(new List.Query{QueryParams = pagingParams}));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMockDomain(Guid id)
    {
        return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
    }

}