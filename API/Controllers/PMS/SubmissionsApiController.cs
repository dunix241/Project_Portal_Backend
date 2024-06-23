using Application.Core;
using Application.Enrollments.Submissions;
using Application.Enrollments.Submissions.Comments;
using Application.Enrollments.Submissions.Comments.DTOs;
using Application.Enrollments.Submissions.DTOs;
using Application.Enrollments.Submissions.Theses;
using Application.Minio.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.PMS;

public class SubmissionsController : PmsApiController
{
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get submission detail")]
    public async Task<ActionResult<DetailEnrollmentSubmission>> GetEnrollmentSubmission(Guid id)
    {
        return HandleResult(await Mediator.Send(new Application.Enrollments.Submissions.DetailEnrollmentSubmission.Query { Id = id }));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Add a submission ")]
    public async Task<IActionResult> CreateSubmission(CreateSubmissionRequestDto payload)
    {
        return HandleResult(await Mediator.Send(new CreateEnrollmentSubmission.Command { Submission = payload }));
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete a submission ")]
    public async Task<IActionResult> DeleteEnrollmentPlan(Guid id)
    {
        return HandleResult(await Mediator.Send(new Application.Enrollments.Submissions.DeleteEnrollmentSubmission.Command { Id = id }));
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Edit a submission ")]
    public async Task<IActionResult> EditSubmission(Guid id , EditSubmissionRequestDto dto)
    {
        return HandleResult(await Mediator.Send(new Application.Enrollments.Submissions.EditEnrollmentSubmission.Command { Id = id , Dto = dto}));
    }

    [HttpPut("{id}/Submit")]
    [SwaggerOperation(Summary = "Submit")]
    public async Task<IActionResult> EditSubmissionThesis(Guid id, IFormFile file)
    {
        var payload = new AddFileRequestDto
        {
            BucketName = "theses",
            FormFile = file,
        };
        return HandleResult(await Mediator.Send(new Application.Enrollments.Submissions.EditEnrollmentSubmissionThesis.Command { Id = id, Dto = payload }));
    }

    [HttpGet]
    [SwaggerOperation(Summary = "List submissions")]
    public async Task<ActionResult<ListEnrollmentSubmission>> ListAllEnrollmentSubmission(Guid enrollmentId)
    {
        return HandleResult(await Mediator.Send(new ListEnrollmentSubmission.Query { EnrollmentId = enrollmentId, }));
    }

    [HttpPut("{id}/Publish")]
    [SwaggerOperation(Summary = "Publish a thesis by submissionId")]
    public async Task<IActionResult> PublishThesis(Guid id)
    {
        return HandleResult(await Mediator.Send(new PublishThesis.Command { Id = id }));
    }

    [HttpDelete("{id}/Publish")]
    [SwaggerOperation(Summary = "Unpublish a thesis by submissionId")]
    public async Task<IActionResult> UnPublishThesis(Guid id)
    {
        return HandleResult(await Mediator.Send(new UnPublishThesis.Command { Id = id }));
    }

}