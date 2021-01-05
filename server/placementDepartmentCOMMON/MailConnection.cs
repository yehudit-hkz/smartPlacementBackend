using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;

namespace placementDepartmentCOMMON
{
    public class MailConnection
    {
        private ExchangeService service;
        private string UserEmail { get; set; }

        public MailConnection(string UserEmail, string ByEmail, string password)
        {
            service = new ExchangeService(ExchangeVersion.Exchange2013)
            {
                Credentials =
                    new WebCredentials(ByEmail, password),
                TraceEnabled = true,
                TraceFlags = TraceFlags.All
            };
            service.AutodiscoverUrl(ByEmail, RedirectionUrlValidationCallback);

            this.UserEmail = UserEmail;
        }

        public void sendMessageToGraduates(string toMail, string subject, string htmlText)
        {
            EmailMessage email = new EmailMessage(service);
            email.From = UserEmail;
            email.ToRecipients.Add(toMail);
            email.Subject = subject;
            email.Body = new MessageBody(htmlText);
            email.Body.BodyType = BodyType.HTML;
            email.SendAndSaveCopy();
        }
        public List<FullGraduateDto> sendCVtoContact(string toMail, string subject, string htmlText, List<FullGraduateDto> graduates, string fldCVPath)
        {
            EmailMessage email = new EmailMessage(service);
            email.From = UserEmail;
            email.ToRecipients.Add(toMail);
            email.Subject = subject;
            email.Body = new MessageBody(htmlText);
            email.Body.BodyType = BodyType.HTML;

            //Add file as attachment.
            List<FullGraduateDto> errCV = new List<FullGraduateDto>();
            foreach (var graduate in graduates)
            {
                var filePath =  fldCVPath + "\\" + graduate.linkToCV;
                email.Attachments.AddFileAttachment($"{graduate.firstName} {graduate.lastName}.pdf", filePath);
            }       

            email.SendAndSaveCopy();
            return errCV;
        }
        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            var redirectionUri = new Uri(redirectionUrl);
            var result = redirectionUri.Scheme == "https";
            return result;
        }
    }
}
