using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PMS;

[AllowAnonymous]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[Controller]/pms")]
public class PmsApiController : BaseApiController
{
}