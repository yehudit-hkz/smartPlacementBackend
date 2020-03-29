﻿using System;
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
            return JobsCoordinationDtoManager.JobsCoordinationDtoListByGraduate(idGraduate);
        }

        [Route("GetByJob")]
        public List<CoordinatingJobsForGraduatesDto> Get(int idJob)
        {
            return JobsCoordinationDtoManager.JobsCoordinationDtoByJob(idJob);
        }

        // POST: api/JobsCoordination
        //public void Post([FromBody]string value)
        //{
        //}

        [Route("Edit")]
        public void Put([FromBody]CoordinatingJobsForGraduatesDto jobsCoordinatingDto)
        {
            JobsCoordinationDtoManager.JobsCoordinationDtoEditing(jobsCoordinatingDto);
        }

        // DELETE: api/JobsCoordination/5
        //public void Delete(int id)
        //{
        //}
    }
}
