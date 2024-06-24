using Application.Core;
using Application.EnrollmentPlans;
using Application.EnrollmentPlans.DTOs;
using Application.EnrollmentPlans.EnrollmentPlanDetails.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CMS;

public class EnrollmentPlansController : CmsApiController
{
    [HttpGet]
    public async Task<ActionResult<ListEnrollmentPlansResponseDto>> ListEnrollmentPlans([FromQuery]PagingParams pagingParams)
    {
        return HandleResult(await Mediator.Send(new List.Query{PagingParams = pagingParams}));
    }

    [HttpPost]
    public async Task<IActionResult> CreateEnrollmentPlan(CreateEnrollmentPlanRequestDto payload)
    {
        return HandleResult(await Mediator.Send(new Create.Command{Payload = payload}));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> EditEnrollmentPlan(Guid id, EditEnrollmentPlanRequestDto payload) {
        return HandleResult(await Mediator.Send(new Edit.Command{Id = id, Payload = payload}));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEnrollmentPlan(Guid id)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }

    [HttpGet("{id}/Details")]
    public async Task<ActionResult<ListEnrollmentPlanDetailsResponseDto>> ListEnrollmentPlanDetails(Guid id, [FromQuery] PagingParams pagingParams)
    {
        return HandleResult(await Mediator.Send(new Application.EnrollmentPlans.EnrollmentPlanDetails.List.Query{EnrollmentPlanId = id, PagingParams = pagingParams}));
    }

    [HttpPost("{id}/Details")]
    public async Task<IActionResult> CreateEnrollmentDetails(Guid id, CreateEnrollmentPlanDetailsRequestDto payload)
    {
        return HandleResult(await Mediator.Send(new Application.EnrollmentPlans.EnrollmentPlanDetails.Create.Command
            { EnrollmentPlanId = id, Payload = payload }));
    }

    [HttpDelete("Details/{id}")]
    public async Task<IActionResult> DeleteEnrollmentDetails(Guid id)
    {
        return HandleResult(await Mediator.Send(new Application.EnrollmentPlans.EnrollmentPlanDetails.Delete.Command
            { Id = id }));
    }
}