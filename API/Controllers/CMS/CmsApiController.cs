using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CMS;

[AllowAnonymous]
[Route("api/[Controller]/cms")]
public class CmsApiController : BaseApiController
{
}