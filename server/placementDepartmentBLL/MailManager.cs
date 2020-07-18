using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public static class MailManager
    {
        static string fontStyle = @"
                        .text{
                            font-family:Arial,Verdana;
                            color:rgb(41, 23, 121);
                        }";
        static string OKBottunCSS = @"
                .myButton {
                        background-color:#6ddf8b;
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
                            background-color: #3aba5c;
                        }
                        .myButton:active {
                            position: relative;
                            top: 1px;
                        }";
        private static string jobOffer(string descJob, string graduateId, string graduateName,bool CV,int idCRD)
        {
            string htmlText = $@"
                    <head>
                        <style>
                            {OKBottunCSS}
                            {fontStyle}
                      </style>
                    </head>
                    <body style='width:60%;text-align:right;' class='text'>";
            htmlText += "בס\"ד<br/><br/>";
            htmlText += $@"שלום {graduateName},<br/>
                        <pre class='text'>
{descJob}

במידה ורלוונטי עבורך, נא אשר\י את מועמדותך:
                       </pre>
                       <div style = 'text-align:center;' >
                            <a href='http://localhost:55968/api/Email?idCRD=" + idCRD + "&idGRD="+graduateId+ $@"&status=נענה%20להצעה'>
                                <button class='myButton'>אישור</button>
                            </a>
                        </div><br/><br/>";
            htmlText += !CV ? "במערכת אין קו\"ח שלך נא צרפ\\י אותם בעת רישומך אחרת לא נוכל לטפל במועמדותך<br/>" : "";
            htmlText +=$@" בברכת הצלחה תמיד<br/>
                        יהודית.<br/>
                    </body>";

            return htmlText;
        }
        public static void sendjobOfferToGraduates(List<CoordinatingJobsForGraduates> coordinatingJob)
        {
            Mail mail = new Mail();
            foreach (var crd in coordinatingJob)
            {
                mail.SendEmailToGraduates(
                    jobOffer(crd.Job.description, crd.candidateId, crd.Graduate.firstName, crd.Graduate.linkToCV != null, crd.Id),
                    crd.Job.title,
                    crd.Graduate.email);
            }
        }
        public static void sendCVCandidateToContact(string massege, List<FullGraduateDto> graduates, string contactMail = "joodyhktz@gmail.com")
        {
            Mail mail = new Mail();
            mail.SendEmailCVtoContact(
                $@"
                    <head>
                        <style>
                            {fontStyle}
                      </style>
                    </head>
                <pre class='text'>{massege}</pre>",
               // "שלום וברכה<br/> מצ\"ב קו\"ח של המעמדים המובחרים במדינה",
                "מועמדים המרכז החרדי",
                graduates.Select(g => g.linkToCV).ToList(),
                contactMail);
        }
    }
}
