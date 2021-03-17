using System.Collections.Generic;
using System.Net.Mail;

namespace VietUSA.Repository.Models
{
    public class EMailModel
    {
        public string FromEmail { get; set; }
        public string FromEmailPassword { get; set; }
        public List<MailAddress> ToEmails { get; set; }
        public int Port { get; set; } = 25;
        public bool UseDefaultCredentials { get; set; } = false;
        public string Host { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int Timeout { get; set; } = 600000;
        public List<string> Attachments { get; set; }
    }
}
