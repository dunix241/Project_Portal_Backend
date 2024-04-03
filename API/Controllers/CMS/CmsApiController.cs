using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CMS;

[AllowAnonymous]
[Route("api/v{version:apiVersion}/[Controller]/cms")]
public class CmsApiController : BaseApiController
{
}