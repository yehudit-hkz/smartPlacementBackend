using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAGetMail;

namespace placementDepartmentBLL
{
    class EmailManneger
    {
        public List<Mail> retrieveNewMailFromGmail(/*int userId*/string adrMail, string password)
        {
            List<Mail> newMails = new List<Mail>();

            try
            {
                // Gmail IMAP4 server is "imap.gmail.com"
                MailServer mailServer = new MailServer(
                                "imap.gmail.com",
                                adrMail,
                                password,
                                ServerProtocol.Imap4);
                // Enable SSL connection.
                mailServer.SSLConnection = true;
                mailServer.Port = 993;

                MailClient mailClient = new MailClient("TryIt");
                mailClient.Connect(mailServer);

                // retrieve unread/new email only
                mailClient.GetMailInfosParam.Reset();
                mailClient.GetMailInfosParam.GetMailInfosOptions = GetMailInfosOptionType.NewOnly;
                MailInfo[] infos = mailClient.GetMailInfos();
                Console.WriteLine("Total {0} unread email(s)\r\n", infos.Length);

                // Receive email list from IMAP4 server
                for (int i = 0; i < infos.Length; i++)
                {
                    MailInfo info = infos[i];
                    Console.WriteLine("Index: {0}; Size: {1}; UIDL: {2}",
                        info.Index, info.Size, info.UIDL);

                    //// Receive email from IMAP4 server
                    Mail oMail = mailClient.GetMail(info);
                    newMails.Add(oMail);

                    // mark unread email as read, next time this email won't be retrieved again
                    if (!info.Read)
                    {
                        mailClient.MarkAsRead(info, true);
                    }
                }

                // Quit and expunge emails marked as deleted from IMAP4 server.
                mailClient.Quit();
                Console.WriteLine("Completed!");
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
            }
            return newMails;
        }
    }
}
