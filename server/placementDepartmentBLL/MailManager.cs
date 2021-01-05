
using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;
using System.Net.Http;

namespace placementDepartmentBLL
{
    public class MailManager
    {
        private string host; //url for link or path for CV file
        private string byEmail;
        private string password;

        public MailManager(string byEmail, string password, string host)
        {
            this.byEmail = byEmail;
            this.password = password;
            this.host = host;

        }

        static string fontStyle = @"
                        .text{
                            font-family:Arial,Verdana;
                            color:rgb(20, 84, 41);
                            font-size:16px;
                        }";
        static string OKBottunCSS = @"
                .myButton {
                        background-color:rgb(250, 221, 75);
                        border:0px;
                        border-radius:5px;
                        display:inline-block;
                        cursor:pointer;
                        color:white;
                        font-size:16px;
                        padding: 10px 32px;
	                    text-decoration:none;
                        }
                        .myButton:hover {
                            background-color: rgb(255, 214, 0);
                        }
                        .myButton:active {
                            position: relative;
                            top: 1px;
                        }";


        private string Activated(string graduateId, string graduateName, bool CV, string sendName)
        {
            string htmlText = $@"
                    <head>
                        <style>
                            {OKBottunCSS}
                            {fontStyle}
                      </style>
                    </head>
                    <body style='width:60%;text-align:right;direction: rtl;' class='text'>";
            htmlText += "בס\"ד<br/><br/>";
            htmlText += $@"שלום {graduateName},<br/>
                        <div style='font-family:inherit;' class='text'>
<br/><br/>
מחלקת ההשמה של המרכז החרדי, מעדכנת את מאגרי המידע אודות התלמידים \ בוגרים בהקשר לנסיונות איתור משרות מתאימות בעתיד.
<br/><br/>
 במדה והנך מעוניין\ת להיכלל במאגר הפעיל כזמין למשרות, נא אשר\י את הרשמתך
<br/><br/>
תוכל לעדכן את הסטטוס בכל עת על ידי שליחת הודעה יזומה למחלקת ההשמה.
<br/><br/>";
            htmlText += $@"</div><br/><div style = 'text-align:center;' >
                            <a href='{host}/api/Email/Activated?idGRD={graduateId}'>
                                <button class='myButton'>אישור</button>
                            </a>
                        </div><br/>";
            htmlText += $@" בברכת הצלחה תמיד<br/>{sendName}
                    </body>";

            return htmlText;
        }

        private string jobOffer(string descJob, string graduateId, string graduateName,bool CV,int idCRD, string sendName)
        {
            string htmlText = $@"
                    <head>
                        <style>
                            {OKBottunCSS}
                            {fontStyle}
                      </style>
                    </head>
                    <body style='width:60%;text-align:right;direction: rtl;' class='text'>";
            htmlText += "בס\"ד<br/><br/>";
            htmlText += $@"שלום {graduateName},<br/>
                        <div class='text'>
<br/>
{descJob}
<br/><br/><br/>
במידה ורלוונטי עבורך, נא אשר\י את מועמדותך:
<br/><br/>";
            htmlText += !CV ? "**במערכת אין קו\"ח שלך נא צרפ\\י אותם בעת רישומך אחרת לא נוכל לטפל במועמדותך<br/>" : "";
            htmlText += $@"</div><br/><div style = 'text-align:center;' >
                            <a href='{host}/api/Email/AcceptedOffer?idCRD=" + idCRD + "&idGRD="+graduateId+ $@"&status=2'>
                                <button class='myButton'>אישור</button>
                            </a>
                        </div><br/>";
            htmlText +=$@" בברכת הצלחה תמיד<br/>{sendName}
                    </body>";

            return htmlText;
        }


        public List<string> sendActivatedToGraduates(List<FullGraduateDto> graduateDtos, int userId)
        {
            UserDto user = UserManager.UserById(userId);
            MailConnection mail = new MailConnection(user.email,byEmail,password);
            List<string> errLine = new List<string>();
            foreach (var graduate in graduateDtos)
            {
                try
                {
                    mail.sendMessageToGraduates(
                      graduate.email,
                      "הצטרפות למחלקת השמה",
                      Activated(graduate.Id, graduate.firstName, graduate.linkToCV != null, user.name)
                      );
                }
                catch (Exception)
                {
                    errLine.Add($"{graduate.firstName} {graduate.lastName}");
                }
            }
            if (errLine.Count == graduateDtos.Count)
                throw new Exception();
            return errLine;
        }
        public List<CoordinatingJobsForGraduates> sendjobOfferToGraduates(List<CoordinatingJobsForGraduates> coordinatingJob, int userId)
        {
            UserDto user = UserManager.UserById(userId);
            MailConnection mail = new MailConnection(user.email,byEmail,password);
            List<CoordinatingJobsForGraduates> errLine = new List<CoordinatingJobsForGraduates>();
            foreach (var crd in coordinatingJob)
            {
                try
                {
                    mail.sendMessageToGraduates(
                    crd.Graduate.email,
                    crd.Job.title,
                    jobOffer(
                        Regex.Replace(crd.Job.description, @"\r\n?|\n", "<br/>"), 
                        crd.candidateId, 
                        crd.Graduate.firstName, 
                        crd.Graduate.linkToCV != null, 
                        crd.Id, 
                        user.name)
                    );
                }
                catch (Exception)
                {
                    errLine.Add(crd);
                }
            }
            if (errLine.Count == coordinatingJob.Count)
                throw new Exception();
            return errLine;
        }
        public List<FullGraduateDto> sendCVCandidateToContact(string title, string massege, List<FullGraduateDto> graduates, string contactMail, int userId)
        {
            UserDto user = UserManager.UserById(userId);
            MailConnection mail = new MailConnection(user.email,byEmail,password);
            List<FullGraduateDto> disSendCV =
            mail.sendCVtoContact(
                    contactMail,
                    "מועמדים המרכז החרדי למשרה " + title,
                    $@"
                    <head>
                        <style>
                            {fontStyle}
                      </style>
                    </head>
                    <div class='text'>{massege}</div>",
                    graduates,
                    host);
            if (disSendCV.Count == graduates.Count)
                throw new Exception();
            return disSendCV;
        }
    }
}
