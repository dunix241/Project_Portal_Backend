using Application.Core;
using Application.Enrollments.Submissions.Comments;
using Application.Enrollments.Submissions.Comments.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.PMS;

public class CommentsController : PmsApiController
{
    [HttpGet]
    [SwaggerOperation(Summary = "List submission comments")]
    public async Task<ActionResult<ListSubmissionComment>> ListSubmissionComment(Guid submissionId, [FromQuery] PagingParams pagingParams)
    {
        return HandleResult(await Mediator.Send(new ListSubmissionComment.Query { SubmissionId = submissionId, PagingParams = pagingParams }));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Add a submission comment")]
    public async Task<IActionResult> CreateSubmissionComment(CreateSubmissionCommentRequest payload)
    {
        return HandleResult(await Mediator.Send(new CreateSubmissionComment.Command { Payload = payload }));
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Edit a  submission comment")]
    public async Task<IActionResult> EditSubmissionComment(Guid id, EditSubmissionCommentRequest payload)
    {
        return HandleResult(await Mediator.Send(new EditSubmissionComment.Command { Id = id, Dto = payload }));
    }

}