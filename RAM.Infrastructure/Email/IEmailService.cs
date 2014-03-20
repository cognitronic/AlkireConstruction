using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RAM.Infrastructure.Email
{
    public interface IEmailService
    {
        bool SendEmail(string from, string to, string subject, string body);

        bool SendEmail(string to, string from, string cc, string bcc, string subject, string body, bool enablessl, string attachmentPath);

        bool SendEmail(string to, string from, string cc, string bcc, string subject, string body, bool enablessl, Stream attachmentPath, string attachmentName);

        bool SendEmail(string to, string from, string subject, string body, string attachmentPath);
    }
}
