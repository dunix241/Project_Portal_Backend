using Application.Core;
using Application.Core.AppSetting;
using Application.Lecturers.Validation;
using AutoMapper;
using Domain.Lecturer;
using Domain.Mail;
using FluentValidation;
using MailKit.Net.Smtp;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mail
{
    public class SendHTMLMail
    {
        public class Command : IRequest<Result<Unit>>
        {
            public HTMLMailData mailData { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly MailSettings _mailSettings;

            public Handler(IOptions<MailSettings> mailSettingsOptions)
            {
                _mailSettings = mailSettingsOptions.Value;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var htmlMailData = request.mailData;

                try
                {             
                    using (MimeMessage emailMessage = new MimeMessage())
                    {
                        MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                        emailMessage.From.Add(emailFrom);

                        MailboxAddress emailTo = new MailboxAddress(htmlMailData.EmailToName, htmlMailData.EmailToId);
                        emailMessage.To.Add(emailTo);

                        emailMessage.Subject = htmlMailData.EmailSubject;

                        string filePath = Directory.GetCurrentDirectory() + "\\Templates\\Template.html";
                        string emailTemplateText = File.ReadAllText(filePath);

                        emailTemplateText = string.Format(emailTemplateText, htmlMailData.EmailToName, htmlMailData.EmailSubject, htmlMailData.EmailBody, DateTime.Today.Date.ToShortDateString());

                        BodyBuilder emailBodyBuilder = new BodyBuilder();
                        emailBodyBuilder.HtmlBody = emailTemplateText;
                        emailBodyBuilder.TextBody = "Plain Text goes here to avoid marked as spam for some email servers.";

                        emailMessage.Body = emailBodyBuilder.ToMessageBody();

                        using (SmtpClient mailClient = new SmtpClient())
                        {
                            mailClient.Connect(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
                            mailClient.Authenticate(_mailSettings.SenderEmail, _mailSettings.Password);
                            mailClient.Send(emailMessage);
                            mailClient.Disconnect(true);
                        }
                    }

                    return Result<Unit>.Success(Unit.Value);
                }
                catch (Exception ex)
                {
                    return Result<Unit>.Failure($"Error sending  HTMLMail: {ex.Message}");
                }
            }
        }
    }
}
