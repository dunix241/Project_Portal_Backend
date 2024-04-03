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

namespace API.Controllers.CMS
{
    public class SchoolsController : CmsApiController
    {
        [HttpPost]
        [SwaggerOperation(Summary = "Add a school")]
        public async Task<IActionResult> CreateSchool(CreateSchoolRequestDto school)
        {
            return HandleResult(await Mediator.Send(new Create.Command { School = school }));
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Edit a school")]
        public async Task<IActionResult> EditSchool(Guid id, EditSchoolRequestDto school)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { Id = id, School = school }));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a school")]
        public async Task<IActionResult> DeleteSchool(Guid id)
        {
            return HandleResult(await Mediator.Send((new Delete.Command { Id = id })));
        }
    }
}
