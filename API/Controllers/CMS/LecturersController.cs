using Application.Core;
using Application.Lecturers;
using Application.Lecturers.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.CMS;

public class LecturersController : CmsApiController
{
    [HttpPost]
    [SwaggerOperation(Summary = "Add a lecturer")]
    public async Task<IActionResult> CreateLecturer(CreateLecturerRequestDto lecturer)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Lecturer = lecturer }));
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a lecturer's information")]
    public async Task<IActionResult> EditLecturer(string id, EditLecturerRequestDto lecturer)
    {
        return HandleResult(await Mediator.Send(new Edit.Command { Id = id, Lecturer = lecturer }));
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove a lecturer")]
    public async Task<IActionResult> DeleteLecturer(string id)
    {
        return HandleResult(await Mediator.Send((new Delete.Command { Id = id })));
    }

    [HttpGet]
    [SwaggerOperation(Summary = "List lecturers")]
    public async Task<IActionResult> ListLectures([FromQuery] ListLecturerRequestDto pagingParams)
    {
        return HandleResult(await Mediator.Send(new List.Query { QueryParams = pagingParams }));
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a lecturer")]
    public async Task<IActionResult> GetLecturer(string id)
    {
        return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
    }

}