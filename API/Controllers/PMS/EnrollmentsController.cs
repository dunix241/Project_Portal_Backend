using Application.Core;
using Application.Enrollment;
using Application.Enrollments;
using Application.Enrollments.DTOs;
using Application.Enrollments.Members;
using Application.Enrollments.Submissions;
using Application.Enrollments.Submissions.Comments;
using Application.Enrollments.Submissions.Comments.DTOs;
using Application.Enrollments.Submissions.DTOs;
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

    [HttpGet("{id}/History")]
    [SwaggerOperation(Summary = "Get enrollment history")]
    public async Task<ActionResult<GetEnrollmentHistoryResponseDto>> ListEnrollmentHistory(Guid id)
    {
        return HandleResult(await Mediator.Send(new GetHistory.Query { ProjectId = id }));
    }

    [HttpGet("{id}/Submissions")]
    [SwaggerOperation(Summary = "Get submission detail")]
    public async Task<ActionResult<GetEnrollmentHistoryResponseDto>> ListEnrollmentSubmission(Guid id)
    {
        return HandleResult(await Mediator.Send(new Application.Enrollments.Submissions.DetailEnrollmentSubmission.Query { Id = id }));
    }

    [HttpPost("Submissios")]
    [SwaggerOperation(Summary = "Add a  submission ")]
    public async Task<IActionResult> CreateSubmission(CreateSubmissionRequestDto payload)
    {
        return HandleResult(await Mediator.Send(new CreateEnrollmentSubmission.Command { Submission = payload }));
    }

    [HttpDelete("{id}/Submmisions")]
    [SwaggerOperation(Summary = "Delete a submission ")]
    public async Task<IActionResult> DeleteEnrollmentPlan(Guid id)
    {
        return HandleResult(await Mediator.Send(new Application.Enrollments.Submissions.DeleteEnrollmentSubmission.Command { Id = id }));
    }

    [HttpPut("{id}/Submmisions")]
    [SwaggerOperation(Summary = "Edit a submission ")]
    public async Task<IActionResult> EditSubmission(Guid id , EditSubmissionRequestDto dto)
    {
        return HandleResult(await Mediator.Send(new Application.Enrollments.Submissions.EditEnrollmentSubmission.Command { Id = id , Dto = dto}));
    }

    [HttpGet("{enrollmentId}/Submissions/List")]
    [SwaggerOperation(Summary = "List submission ")]
    public async Task<ActionResult<ListEnrollmentSubmission>> ListEnrollmentSubmission(Guid enrollmentId, [FromQuery] PagingParams pagingParams)
    {
        return HandleResult(await Mediator.Send(new ListEnrollmentSubmission.Query { EnrollmentId = enrollmentId, QueryParams = pagingParams }));
    }

    [HttpGet("Submissions/{id}/Comments")]
    [SwaggerOperation(Summary = "List submission comment ")]
    public async Task<ActionResult<ListEnrollmentSubmission>> ListSubmissionComment(Guid id, [FromQuery] PagingParams pagingParams)
    {
        return HandleResult(await Mediator.Send(new ListSubmissionComment.Query { SubmissionId = id, PagingParams = pagingParams }));
    }

    [HttpPost("Submission/Comments")]
    [SwaggerOperation(Summary = "Add a  submission comment")]
    public async Task<IActionResult> CreateSubmissionComment(CreateSubmissionCommentRequest payload)
    {
        return HandleResult(await Mediator.Send(new CreateSubmissionComment.Command { Payload = payload }));
    }

}