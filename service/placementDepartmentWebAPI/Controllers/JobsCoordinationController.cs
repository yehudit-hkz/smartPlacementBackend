using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using placementDepartmentCOMMON;
using placementDepartmentBLL;

namespace placementDepartmentWebAPI.Controllers
{
    [RoutePrefix("api/JobsCoordination")]
    public class JobsCoordinationController : ApiController
    {
        [Route("GetByGraduate")]
        public List<CoordinatingJobsForGraduatesDto> Get(string idGraduate)
        {
            try
            {
                return JobsCoordinationDtoManager.JobsCoordinationDtoListByGraduate(idGraduate);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("GetByJob")]
        public List<CoordinatingJobsForGraduatesDto> Get(int idJob)
        {
            try
            {
                return JobsCoordinationDtoManager.JobsCoordinationDtoByJob(idJob);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        //POST: api/JobsCoordination
        [Route("Save")]
        public void Post(int idJob,[FromBody]List<FullGraduateDto> value)
        {
            try
            {
                JobsCoordinationDtoManager.NewJobsCoordinationDto(idJob, value);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("Edit")]
        public void Put([FromBody]CoordinatingJobsForGraduatesDto jobsCoordinatingDto)
        {
            try
            {
                JobsCoordinationDtoManager.JobsCoordinationDtoEditing(jobsCoordinatingDto);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("sendCV")]
        public void Put(string massege, [FromBody]List<CoordinatingJobsForGraduatesDto> coordinatings)
        {
            try
            {
                JobsCoordinationDtoManager.sendingCandidateToContact(massege, coordinatings);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/JobsCoordination/5
        //public void Delete(int id)
        //{
        //}
    }
}
