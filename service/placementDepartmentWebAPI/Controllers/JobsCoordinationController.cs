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
    public class JobsCoordinationController : ApiController
    {
       //// GET: api/JobsCoordination
        public List<CoordinatingJobsForGraduatesDto> Get(string graduateId)
        {
            return null;
        }

        //// GET: api/JobsCoordination/5
        public List<CoordinatingJobsForGraduatesDto> Get(int jobId)
        {
            return null;
        }

        // POST: api/JobsCoordination
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: api/JobsCoordination/5
        public void Put([FromBody]CoordinatingJobsForGraduatesDto jobsCoordinatingDto)
        {

        }

        // DELETE: api/JobsCoordination/5
        //public void Delete(int id)
        //{
        //}
    }
}
