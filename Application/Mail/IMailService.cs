using Domain.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mail
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
        bool SendHTMLMail(HTMLMailData htmlMailData);
    }
}
