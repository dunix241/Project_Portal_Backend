using Application.Core;
using Application.Schools.DTOs;
using Application.Schools;
using Domain.School;
using Microsoft.AspNetCore.Mvc;
using Application.MockDomains;
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
        public async Task<IActionResult> GetSchools([FromQuery] PagingParams pagingParams)
        {
            return HandleResult(await Mediator.Send(new List.Query { QueryParams = pagingParams }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchool(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }
        [HttpPost]
        public async Task<IActionResult> CreateSChool(CreateSchoolRequestDto school)
        {
            return HandleResult(await Mediator.Send(new Create.Command { School = school }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditSchool(Guid id, EditSchoolRequestDto school)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { Id = id, School = school }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(Guid id)
        {
            return HandleResult(await Mediator.Send((new Delete.Command { Id = id })));
        }


    }
}
