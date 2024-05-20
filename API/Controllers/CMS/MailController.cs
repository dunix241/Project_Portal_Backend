using Application.Mail;
using Domain.Mail;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CMS
{
    public class MailController : ApiController
    {

        [HttpPost]
        [Route("SendHTMLMail")]
        public async Task<IActionResult> SendHTMLMail(MailData mailData)
        {
            var result = await Mediator.Send(new Send.Command { MailData = mailData });
            return HandleResult(result);
        }

    }
}
