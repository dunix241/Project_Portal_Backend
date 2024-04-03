using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[Controller]")]
public class ApiController : BaseApiController
{
}