using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;
using placementDepartmentCOMMON;

namespace placementDepartmentWebClient
{
    public class MailConnection
    {
        private ExchangeService service;
        public string UserEmail { get; set; }

        public MailConnection(string UserEmail)
        {
            service = new ExchangeService(ExchangeVersion.Exchange2013)
            {
                Credentials =
                    new WebCredentials("NoReply@CharediCTS.org.il", "n4501"),
                TraceEnabled = true,
                TraceFlags = TraceFlags.All
            };
            service.AutodiscoverUrl("NoReply@CharediCTS.org.il",
                RedirectionUrlValidationCallback);

            ////impersonate user e.g. by specifying an SMTP address:
            //service.ImpersonatedUserId = new ImpersonatedUserId(
            //    ConnectingIdType.SmtpAddress, "test@charedicts.org.il");

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

            var userMailbox = new Mailbox("test@charedicts.org.il");
            var folderId = new FolderId(WellKnownFolderName.SentItems, userMailbox);

            email.SendAndSaveCopy(folderId);
        }
        public List<FullGraduateDto> sendCVtoContact(string toMail, string subject, string htmlText, List<FullGraduateDto> graduates)
        {
            EmailMessage email = new EmailMessage(service);
            email.From = UserEmail;
            email.ToRecipients.Add(toMail);
            email.Subject = subject;
            email.Body = new MessageBody(htmlText);
            email.Body.BodyType = BodyType.HTML;

            //Add file as attachment.
            List<FullGraduateDto> nullCV = new List<FullGraduateDto>();
            foreach (var graduate in graduates)
            {
                    if(graduate.linkToCV != null) { 
                        var filePath = HttpContext.Current.Server.MapPath("~/" + graduate.linkToCV);
                        email.Attachments.AddFileAttachment($"{graduate.firstName} {graduate.lastName}.pdf", filePath);
                    }
                    else
                    {
                        nullCV.Add(graduate);
                    }
            }

            email.SendAndSaveCopy();
            return nullCV;
        }
        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            var redirectionUri = new Uri(redirectionUrl);
            var result = redirectionUri.Scheme == "https";
            return result;
        }
    }
}
