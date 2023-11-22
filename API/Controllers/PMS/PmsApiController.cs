using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PMS;

[AllowAnonymous]
[Route("api/[Controller]/pms")]
public class PmsApiController : BaseApiController
{
}