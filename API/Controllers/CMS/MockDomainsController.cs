using Application.MockDomains;
using Application.MockDomains.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CMS;

public class MockDomainsController : CmsApiController
{
    //[HttpPost]
    //public async Task<IActionResult> CreateMockDomain(CreateMockDomainRequestDto mockDomain)
    //{
    //    return HandleResult(await Mediator.Send(new Create.Command { Lecturer = mockDomain }));
    //}

    [HttpPut("{id}")]
    public async Task<IActionResult> EditMockDomain(Guid id, EditMockDomainRequestDto mockDomain)
    {
        return HandleResult(await Mediator.Send(new Edit.Command { Id = id, MockDomain = mockDomain }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMockDomain(Guid id)
    {
        return HandleResult(await Mediator.Send((new Delete.Command { Id = id })));
    }

}