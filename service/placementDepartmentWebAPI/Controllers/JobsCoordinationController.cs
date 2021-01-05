using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using placementDepartmentCOMMON;
using placementDepartmentBLL;
using System.Web;
using System.Configuration;

namespace placementDepartmentWebAPI.Controllers
{
    [RoutePrefix("api/JobsCoordination")]
    public class JobsCoordinationController : ApiController
    {
        [Route("GetLazyList")]
        public ApiRes<CoordinatingJobsForGraduatesDto> Get(int page, int size, string sort, [FromUri]JobCoordinationFilters filters)
        {
            filters.curUserId = Int32.Parse(HttpContext.Current.User.Identity.Name);
            return JobsCoordinationDtoManager.JobCoordinationDtoLazyList(filters, sort, page, size);
        }
        [Route("GetByGraduate")]
        public List<CoordinatingJobsForGraduatesDto> Get(string idGraduate)
        {
            int userId = Int32.Parse(HttpContext.Current.User.Identity.Name);
            return JobsCoordinationDtoManager.JobsCoordinationDtoListByGraduate(idGraduate,userId);
        }

        [Route("GetByJob")]
        public List<CoordinatingJobsForGraduatesDto> Get(int idJob)
        {
            return JobsCoordinationDtoManager.JobsCoordinationDtoByJob(idJob);
        }

        [Route("Save")]
        public List<string> Post(int idJob, [FromBody]List<FullGraduateDto> value)
        {
            var userId = Int32.Parse(HttpContext.Current.User.Identity.Name);
            string email = ConfigurationManager.AppSettings["byEmail"];
            string password = ConfigurationManager.AppSettings["password"];
            var url =  HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.Substring(0, url.IndexOf("/api"));

            return JobsCoordinationDtoManager.NewJobsCoordinationDto(idJob, value, userId,email,password,url);
        }

        [Route("Edit")]
        public void Put([FromBody]CoordinatingJobsForGraduatesDto jobsCoordinatingDto)
        {
            JobsCoordinationDtoManager.JobsCoordinationDtoEditing(jobsCoordinatingDto);
        }

        [Route("sendCV")]
        public List<FullGraduateDto> Put(string massege, [FromBody]List<CoordinatingJobsForGraduatesDto> coordinatings)
        {
            var userId = Int32.Parse(HttpContext.Current.User.Identity.Name);
            string email = ConfigurationManager.AppSettings["byEmail"];
            string password = ConfigurationManager.AppSettings["password"];
            string folderPeth = ConfigurationManager.AppSettings["CVpath"];

            return JobsCoordinationDtoManager.sendingCandidateToContact(massege, coordinatings, userId,email,password,folderPeth);
        }
    }
}
