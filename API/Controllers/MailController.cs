using Application.Mail;
using Domain.Mail;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MailController : ApiController
    {
        private readonly IMailService _mailService;
        public MailController(IMailService _MailService)
        {
            _mailService = _MailService;
        }

        [HttpPost]
        [Route("SendMail")]
        public bool SendMail(MailData mailData)
        {
            return _mailService.SendMail(mailData);
        }

        [HttpPost]
        [Route("SendHTMLMail")]
        public bool SendHTMLMail(HTMLMailData htmlMailData)
        {
            return _mailService.SendHTMLMail(htmlMailData);
        }

    }
}
