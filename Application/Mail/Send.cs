using Application.Core;
using Application.Core.AppSetting;
using Domain.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Application.Mail
{
    public class Send
    {
        public class Command : IRequest<Result<Unit>>
        {
            public MailData MailData { get; set; }
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
                var htmlMailData = request.MailData;

                try
                {             
                    using (MimeMessage emailMessage = new MimeMessage())
                    {
                        MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                        emailMessage.From.Add(emailFrom);

                        MailboxAddress emailTo = new MailboxAddress(htmlMailData.ToName, htmlMailData.ToAddress);
                        emailMessage.To.Add(emailTo);
                        emailMessage.Subject = htmlMailData.Subject;
                        emailMessage.Body = htmlMailData.BodyBuilder.ToMessageBody();

                        using (SmtpClient mailClient = new SmtpClient())
                        {
                            await mailClient.ConnectAsync(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.SslOnConnect);
                            await mailClient.AuthenticateAsync(_mailSettings.SenderEmail, _mailSettings.Password);
                            await mailClient.SendAsync(emailMessage);
                            await mailClient.DisconnectAsync(true);
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
