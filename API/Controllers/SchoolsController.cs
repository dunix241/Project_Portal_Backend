using API.Controllers.CMS;
using Application.Core;
using Application.Schools.DTOs;
using Application.Schools;
using Domain.School;
using Microsoft.AspNetCore.Mvc;
using Application.MockDomains;
using Swashbuckle.AspNetCore.Annotations;
using List = Application.Schools.List;
using Details = Application.Schools.Details;
using Create = Application.Schools.Create;
using Edit = Application.Schools.Edit;
using Delete = Application.Schools.Delete;

namespace API.Controllers
{
    public class SchoolsController : ApiController
    {
        [HttpGet]
        [SwaggerOperation(Summary = "List schools")]
        public async Task<IActionResult> ListSchools([FromQuery] PagingParams pagingParams)
        {
            return HandleResult(await Mediator.Send(new List.Query { QueryParams = pagingParams }));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a school")]
        public async Task<IActionResult> GetSchool(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }
    }
}
