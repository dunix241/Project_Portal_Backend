using MimeKit;

namespace Domain.Mail
{
    public class MailData
    {
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string ToName { get; set; }
        public BodyBuilder BodyBuilder { get; set; }
    }
}
