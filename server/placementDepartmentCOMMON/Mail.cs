using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Net.Mail;
using System.Web;

namespace placementDepartmentCOMMON
{
    public class Mail
    {
        static string fromEmail = "mysmartplacement@gmail.com";
        static string password = "plc/1234";
        private SmtpClient smtp;

        public Mail()
        {
            smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = false,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, password)// Enter senders User name and password
            };
        }

        public void SendEmailToGraduates(string htmlText, string subject, string toMail )
        {
            try
            {
                AlternateView plainView =
                    AlternateView.CreateAlternateViewFromString("Some plaintext", Encoding.UTF8, "text/plain");
                // We have something to show in real old mail clients.
                smtp.EnableSsl = true;
                MailMessage mail = new MailMessage(fromEmail, toMail, subject, htmlText);
                mail.AlternateViews.Add(plainView);
                AlternateView htmlView =
                       AlternateView.CreateAlternateViewFromString(htmlText, Encoding.UTF8, "text/html");
                mail.AlternateViews.Add(htmlView); // And a html attachment to make sure.
                mail.IsBodyHtml = true;
                mail.BodyEncoding = UTF8Encoding.UTF8;

                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SendEmailCVtoContact(string htmlText, string subject,List<string> linkToCVs, string contactMail)
        {
            try
            {
                AlternateView plainView = AlternateView
                .CreateAlternateViewFromString("Some plaintext", Encoding.UTF8, "text/plain");
                // We have something to show in real old mail clients.
                smtp.EnableSsl = true;
                MailMessage mail = new MailMessage(fromEmail, contactMail, subject, htmlText);
                mail.AlternateViews.Add(plainView);
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                //Add file as attachment.
                //var linkToCV = "ResumeFile/123456795.pdf";
                foreach (var linkToCV in linkToCVs)
                {
                    var filePath = HttpContext.Current.Server.MapPath("~/" + linkToCV);
                    mail.Attachments.Add(new Attachment(filePath));
                }
                
               AlternateView htmlView =
                       AlternateView.CreateAlternateViewFromString(htmlText, Encoding.UTF8, "text/html");
                mail.AlternateViews.Add(htmlView); // And a html attachment to make sure.
                mail.IsBodyHtml = true;
                mail.BodyEncoding = UTF8Encoding.UTF8;
                
                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }

        }
      

    }
}
