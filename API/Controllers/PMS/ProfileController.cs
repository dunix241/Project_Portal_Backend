using Application.Core;
using Application.Enrollments.Submissions.Comments;
using Application.Enrollments.Submissions;
using Application.Lecturers.DTOs;
using Application.Users.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Application.Projects.DTOs;
using Application.Users;

namespace API.Controllers.PMS
{
    public class ProfileController : PmsApiController
    {

        [HttpGet("Profiles/{id}")]
        [SwaggerOperation(Summary = "List submission comment ")]
        public async Task<ActionResult<ListEnrollmentSubmission>> ListSubmissionComment(string id)
        {
            return HandleResult(await Mediator.Send(new Application.Users.Details.Query {Id = id }));
        }

        [HttpPut("Profiles{id}")]
        public async Task<IActionResult> Edit(string id, EditUserRequest project)
        {
            return HandleResult(await Mediator.Send(new EditUser.Command { Id = id, request = project }));
        }
    }
}
