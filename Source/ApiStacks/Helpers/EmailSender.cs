
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ApiStacks
{
    public class EmailSender
    {
        public static void SendEmail(Model_Email email, HttpServerUtility Server)
        {
            //Send email
            string sampleFilePathpath = Server.MapPath("/templates/email.html");
            using (var sr = new StreamReader(sampleFilePathpath))
            {
                var content = sr.ReadToEnd();

                MailMessage message = new MailMessage();
                message.From = new MailAddress("donotreply@apistacks.com");
                message.To.Add(email.email);
                message.Subject = email.subject;
                message.IsBodyHtml = true;
                message.Body = content.Replace("{{NAME}}", email.name).Replace("{{BODY}}", email.body);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.UseDefaultCredentials = true;

                smtpClient.Host = "apistacks.com";
                smtpClient.Port = 25;
                //smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("donotreply@apistacks.com", "Tawanda_91!");
                smtpClient.Send(message);
            }
        }
    }
}