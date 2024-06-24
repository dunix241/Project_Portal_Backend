using Application.Core;
using Application.Enrollment;
using Application.Enrollments;
using Application.Enrollments.DTOs;
using Application.Enrollments.Members;
using Application.Enrollments.Submissions;
using Application.Enrollments.Submissions.Comments;
using Application.Enrollments.Submissions.Comments.DTOs;
using Application.Enrollments.Submissions.DTOs;
using Application.Enrollments.Submissions.Theses;
using Application.Minio.DTOs;
using Domain.Enrollment;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.PMS;

public class EnrollmentsController : PmsApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateEnrollment(CreateEnrollmentRequestDto payload)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Payload = payload }));
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "edit an enrollment")]
    public async Task<IActionResult> EditEnrollment(Guid id, EditEnrollmentRequestDto payload)
    {
        return HandleResult(await Mediator.Send(new Edit.Command { Id = id, Payload = payload }));
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "get enrollment details of the current project")]
    public async Task<IActionResult> GetEnrollment(Guid id)
    {
        return HandleResult(await Mediator.Send(new Get.Query { Id = id }));
    }

    [HttpGet("{id}/Members")]
    public async Task<ActionResult<ListEnrollmentMembersResponseDto>> ListEnrollmentMembers(Guid id)
    {
        return HandleResult(await Mediator.Send(new ListEnrollmentMembers.Query { Id = id }));
    }

    [HttpPost("{id}/Members")]
    public async Task<ActionResult<ListEnrollmentMembersResponseDto>> CreateEnrollmentMember(Guid id, CreateEnrollmentMemberRequestDto payload)
    {
        return HandleResult(await Mediator.Send(new CreateEnrollmentMember.Command
        {
            EnrollmentId = id,
            Payload = payload
        }));
    }

    [HttpPut("Members/{id}")]
    public async Task<IActionResult> EditEnrollmentMember(Guid id, EditEnrollmentMemberRequestDto payload)
    {
        return HandleResult(await Mediator.Send(new EditEnrollmentMember.Command { Id = id, Payload = payload }));
    }

    [HttpDelete("Members/{id}")]
    public async Task<IActionResult> DeleteEnrollmentMember(Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteEnrollmentMember.Command { Id = id }));
    }

    [HttpGet("History")]
    [SwaggerOperation(Summary = "Get enrollment history of current user")]
    public async Task<ActionResult<GetEnrollmentHistoryResponseDto>> ListEnrollmentHistory(Guid enrollmentId)
    {
        return HandleResult(await Mediator.Send(new GetHistory.Query { EnrollmentId = enrollmentId }));
    }

    [HttpGet("Joined")]
    public async Task<ActionResult<ListProjectsJoinedResponseDto>> GetProjectJoined(string userId)
    {
        return HandleResult(await Mediator.Send(new ListProjectsJoined.Query()));
    }
}