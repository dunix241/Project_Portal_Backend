using MimeKit;

namespace Domain.Mail
{
    public class HTMLMailData
    {
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ToName { get; set; }
        public BodyBuilder BodyBuilder { get; set; }
    }
}
