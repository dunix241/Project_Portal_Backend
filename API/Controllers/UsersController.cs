using Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : ApiController
{
    [HttpPut]
    public async Task<IActionResult> UpdateBio([FromBody] string bio)
    {
        return HandleResult(await Mediator.Send(new EditBio.Command { Bio = bio }));
    }
}