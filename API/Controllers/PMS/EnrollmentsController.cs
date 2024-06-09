using Application.Enrollment;
using Application.Enrollments;
using Application.Enrollments.DTOs;
using Application.Enrollments.Members;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PMS;

public class EnrollmentsController : PmsApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateEnrollment(CreateEnrollmentRequestDto payload)
    {
        return HandleResult(await Mediator.Send(new Create.Command{Payload = payload}));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> EditEnrollment(Guid id, EditEnrollmentRequestDto payload)
    {
        return HandleResult(await Mediator.Send(new Edit.Command{Id = id, Payload = payload}));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEnrollment(Guid id)
    {
        return HandleResult(await Mediator.Send(new Get.Query{Id = id}));
    }
    
    [HttpGet("{id}/Members")]
    public async Task<ActionResult<ListEnrollmentMembersResponseDto>> ListEnrollmentMembers(Guid id)
    {
        return HandleResult(await Mediator.Send(new ListEnrollmentMembers.Query{Id = id}));
    }
    
    [HttpPost("{id}/Members")]
    public async Task<ActionResult<ListEnrollmentMembersResponseDto>> CreateEnrollmentMember(Guid id, CreateEnrollmentMemberRequestDto payload)
    {
        return HandleResult(await Mediator.Send(new CreateEnrollmentMember.Command
        {
            EnrollmentId = id, Payload = payload
        }));
    }
    
    [HttpPut("Members/{id}")]
    public async Task<IActionResult> EditEnrollmentMember(Guid id, EditEnrollmentMemberRequestDto payload)
    {
        return HandleResult(await Mediator.Send(new EditEnrollmentMember.Command{Id = id, Payload = payload}));
    }
    
    [HttpDelete("Members/{id}")]
    public async Task<IActionResult> DeleteEnrollmentMember(Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteEnrollmentMember.Command{Id = id}));
    }
}