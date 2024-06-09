using Application.Semesters;
using Application.Semesters.DTOs;
using Application.Semesters.DTOs.Projects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.CMS;

public class SemestersController : CmsApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateSemesterRequestDto semester)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Semester = semester }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(Guid id, EditSemesterRequestDto semester)
    {
        return HandleResult(await Mediator.Send(new Edit.Command { Id = id, Semester = semester }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return HandleResult(await Mediator.Send((new Delete.Command { Id = id })));
    }

    [HttpPost("{id}/Projects")]
    [SwaggerOperation(Summary = "Assign a project semester ?")]
    public async Task<IActionResult> AddProject(Guid id, SemesterCreateProjectRequestDto projectSemester)
    {
        return HandleResult(await Mediator.Send(new Application.Semesters.Projects.Create.Command
            { SemesterId = id, ProjectSemester = projectSemester }));
    }

    [HttpPut("{id}/Projects/{projectId}")]
    public async Task<IActionResult> EditProjectSemester(Guid id, Guid projectId,
        SemesterEditProjectRequestDto projectSemester)
    {
        return HandleResult(await Mediator.Send(new Application.Semesters.Projects.Edit.Command
            { SemesterId = id, ProjectId = projectId, ProjectSemester = projectSemester }));
    }

    [HttpDelete("{id}/Projects/{projectId}")]
    public async Task<IActionResult> DeleteProject(Guid id, Guid projectId)
    {
        return HandleResult(await Mediator.Send((new Application.Semesters.Projects.Delete.Command
            { SemesterId = id, ProjectId = projectId })));
    }
}