using Application.Lecturers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.PMS
{
    public class LecturersController : PmsApiController
    {
        [HttpGet]
        [SwaggerOperation(Summary = "Get Lecturer Profile")]
        public async Task<IActionResult> CreateSchool(string id)
        {
            return HandleResult(await Mediator.Send(new CMSDetail.Query { Id = id }));
        }
    }
}
