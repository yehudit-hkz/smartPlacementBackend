using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using placementDepartmentCOMMON;
using placementDepartmentBLL;

namespace placementDepartmentWebAPI.Controllers
{
    public class EmailController : ApiController
    {
        // GET: api/Email
        public HttpResponseMessage Get(int idCRD,string idGRD , string status)
        {
            JobsCoordinationDtoManager.JobsCoordinationDtoUpdate(idCRD, status);
            string logicalPath = System.Web.HttpContext.Current.Server.MapPath("~/Views/Email/OKJobRes.cshtml");
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(System.IO.File.ReadAllText(logicalPath).Replace("000000000", idGRD))
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html") { CharSet = "UTF8" };
            return response;
        }

        // GET: api/Email/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Email
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Email/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Email/5
        public void Delete(int id)
        {
        }
    }
}
