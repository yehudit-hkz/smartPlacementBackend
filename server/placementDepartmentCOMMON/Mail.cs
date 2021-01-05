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
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, password)// Enter senders User name and password
            };
        }

        public void SendEmailToGraduates(string htmlText, string subject, string toMail)
        {
            try
            {
                MailMessage mail = new MailMessage(fromEmail, toMail, subject, htmlText);

                AlternateView plainView =
                   AlternateView.CreateAlternateViewFromString("Some plaintext", Encoding.UTF8, "text/plain");
                mail.AlternateViews.Add(plainView);
                AlternateView htmlView =
                       AlternateView.CreateAlternateViewFromString(htmlText, Encoding.UTF8, "text/html");
                mail.AlternateViews.Add(htmlView); // And a html attachsmtpment to make sure.

                mail.IsBodyHtml = true;
                mail.BodyEncoding = UTF8Encoding.UTF8;
                mail.SubjectEncoding = UTF8Encoding.UTF8;

                smtp.EnableSsl = true;

                smtp.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<FullGraduateDto> SendEmailCVtoContact(string htmlText, string subject, List<FullGraduateDto> graduates, string contactMail)
        {
            List<FullGraduateDto> errCV = new List<FullGraduateDto>();
            try
            {
                MailMessage mail = new MailMessage(fromEmail, contactMail, subject, htmlText);

                AlternateView plainView =
                     AlternateView.CreateAlternateViewFromString("Some plaintext", Encoding.UTF8, "text/plain");
                mail.AlternateViews.Add(plainView);
                AlternateView htmlView =
                      AlternateView.CreateAlternateViewFromString(htmlText, Encoding.UTF8, "text/html");
                mail.AlternateViews.Add(htmlView); // And a html attachment to make sure.

                //Add file as attachment.
                foreach (var graduate in graduates)
                {
                    try
                    {
                        var filePath = HttpContext.Current.Server.MapPath("~/" + graduate.linkToCV);
                        mail.Attachments.Add(new Attachment(filePath));
                    }
                    catch (Exception)
                    {
                        errCV.Add(graduate);
                    }

                }

                mail.IsBodyHtml = true;
                mail.BodyEncoding = UTF8Encoding.UTF8;
                mail.SubjectEncoding = UTF8Encoding.UTF8;

                smtp.EnableSsl = true;

                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
            return errCV;
        }


    }
}
