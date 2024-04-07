using Application.Core;
using Application.Mail;
using Domain.Lecturer;
using Domain.Mail;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MailController : ApiController
    {

        [HttpPost]
        [Route("SendHTMLMail")]
        public async Task<IActionResult> SendHTMLMail(HTMLMailData htmlMailData)
        {
            var result = await Mediator.Send(new SendHTMLMail.Command { mailData = htmlMailData });

            if (result.IsSuccess)
            {
                return Ok("The email has been sent successfully ");
            }

            return BadRequest(result.Error);
        }

    }
}
