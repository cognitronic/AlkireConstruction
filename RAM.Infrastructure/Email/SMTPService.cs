using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.Net.Security;
using System.Net;
using System.IO;
using RAM.Infrastructure.Configuration;

namespace RAM.Infrastructure.Email
{
    public class SMTPService : IEmailService
    {
        public bool SendEmail(string to, string from, string cc, string bcc, string subject, string body, bool enablessl, string attachmentPath)
        {
            System.Net.Mail.SmtpClient cl = new System.Net.Mail.SmtpClient();
            cl.Host = ApplicationSettingsFactory.GetApplicationSettings().SmtpHost;
            cl.UseDefaultCredentials = false;
            cl.Credentials = new System.Net.NetworkCredential(ApplicationSettingsFactory.GetApplicationSettings().SmtpUsername, ApplicationSettingsFactory.GetApplicationSettings().SmtpPassword);
            cl.EnableSsl = enablessl;
            cl.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            try
            {

                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(from, to, subject, body);

                // CC and BCC optional
                if (!string.IsNullOrEmpty(cc))
                {
                    string[] ccArray = cc.Split(',');
                    foreach (string sCC in ccArray)
                    {
                        msg.CC.Add(sCC);
                    }
                }

                // You can specify Address directly as string
                if (!string.IsNullOrEmpty(bcc))
                {
                    string[] bccArray = bcc.Split(',');
                    foreach (string sBCC in bccArray)
                    {
                        msg.Bcc.Add(sBCC);
                    }
                }

                if (!string.IsNullOrEmpty(attachmentPath))
                {
                    var att = new System.Net.Mail.Attachment(attachmentPath);
                    msg.Attachments.Add(att);
                }
                msg.IsBodyHtml = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                cl.Send(msg);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool SendEmail(string to, string from, string cc, string bcc, string subject, string body, bool enablessl, Stream attachmentPath, string attachmentName)
        {
            System.Net.Mail.SmtpClient cl = new System.Net.Mail.SmtpClient();
            cl.Host = ApplicationSettingsFactory.GetApplicationSettings().SmtpHost;
            cl.UseDefaultCredentials = false;
            cl.Credentials = new System.Net.NetworkCredential(ApplicationSettingsFactory.GetApplicationSettings().SmtpUsername, ApplicationSettingsFactory.GetApplicationSettings().SmtpPassword);
            cl.EnableSsl = enablessl;
            cl.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            try
            {

                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(from, to, subject, body);

                // CC and BCC optional
                if (!string.IsNullOrEmpty(cc))
                {
                    string[] ccArray = cc.Split(',');
                    foreach (string sCC in ccArray)
                    {
                        msg.CC.Add(sCC);
                    }
                }

                // You can specify Address directly as string
                if (!string.IsNullOrEmpty(bcc))
                {
                    string[] bccArray = bcc.Split(',');
                    foreach (string sBCC in bccArray)
                    {
                        msg.Bcc.Add(sBCC);
                    }
                }

                if (attachmentPath != null)
                {
                    var att = new System.Net.Mail.Attachment(attachmentPath, attachmentName);
                    msg.Attachments.Add(att);
                }
                msg.IsBodyHtml = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                cl.Send(msg);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool SendEmail(string to, string from, string subject, string body)
        {
            return SendEmail(to, from, "", "", subject, body, false, "");
        }

        public bool SendEmail(string to, string from, string subject, string body, string attachmentPath)
        {
            return SendEmail(to, from, "", "", subject, body, false, attachmentPath);
        }
    }
}
