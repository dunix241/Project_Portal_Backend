using Application.Projects;
using Application.Projects.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CMS;

public class ProjectsController : CmsApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectRequestDto project)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Project = project }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(Guid id, EditProjectRequestDto project)
    {
        return HandleResult(await Mediator.Send(new Edit.Command { Id = id, Project = project}));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return HandleResult(await Mediator.Send((new Delete.Command { Id = id })));
    }
}