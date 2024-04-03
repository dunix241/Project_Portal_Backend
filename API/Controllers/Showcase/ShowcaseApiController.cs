using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Showcase;

[AllowAnonymous]
[Route("api/v{version:apiVersion}/[Controller]/showcase")]
public class ShowcaseApiController : BaseApiController
{
}