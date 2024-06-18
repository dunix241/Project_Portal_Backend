using Application.Core;
using Application.EnrollmentPlans.DTOs;
using Application.Overview;
using Application.Overview.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CMS
{
    public class OverviewController : CmsApiController
    {
        [HttpGet]
        public async Task<ActionResult<OverviewResponse>> ListEnrollmentPlans()
        {
            return HandleResult(await Mediator.Send(new GetOverview.Query { }));
        }
    }     
}
