using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using placementDepartmentCOMMON;
using placementDepartmentBLL;
using System.Web;

namespace placementDepartmentWebAPI.Controllers
{
    [RoutePrefix("api/Email")]
    public class EmailController : ApiController
    {
        [Route("AcceptedOffer")]
        public HttpResponseMessage Get(int idCRD, string idGRD , int status)
        {
            try
            {
                JobsCoordinationDtoManager.JobsCoordinationDtoUpdate(idCRD, status);

                var host = HttpContext.Current.Request.Url.AbsoluteUri;
                host = host.Substring(0, host.IndexOf("/api"));

                string logicalPath = System.Web.HttpContext.Current.Server.MapPath("~/Views/Email/OKJobRes.cshtml");
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        System.IO.File.ReadAllText(logicalPath)
                        .Replace("000000000", idGRD)
                        .Replace("XXXXXXXX", host)
                        )
                };
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html") { CharSet = "UTF8" };
                return response;
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("Activated")]
        public HttpResponseMessage Get(string idGRD)
        {
            try
            {
                GraduateDtoManager.GraduateDtoEditingtrue(idGRD,true);

                var host = HttpContext.Current.Request.Url.AbsoluteUri;
                host = host.Substring(0, host.IndexOf("/api"));

                string logicalPath = System.Web.HttpContext.Current.Server.MapPath("~/Views/Email/OKActive.cshtml");
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        System.IO.File.ReadAllText(logicalPath)
                        .Replace("000000000", idGRD)
                        .Replace("XXXXXXXX", host)
                        )
                };
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html") { CharSet = "UTF8" };
                return response;
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
