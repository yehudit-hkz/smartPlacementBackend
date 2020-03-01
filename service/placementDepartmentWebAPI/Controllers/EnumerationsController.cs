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
    [RoutePrefix("api/List")]

    public class EnumerationsController : ApiController
    {
        [Route("GetCities")]
        [HttpGet]
        public List<CityDto> GetCities()
        {
            return EnumerationsDtoManager.CityDtoList();
        }
        [Route("GetLanguages")]
        [HttpGet]
        public List<LanguageDto> GetLanguages()
        {
            return EnumerationsDtoManager.LanguageDtoList();
        }
        [Route("GetReasonForClosing")]
       [HttpGet]
        public List<ReasonForClosingThePositionDto> GetReasonForClosing()
        {
            return EnumerationsDtoManager.ReasonForClosingDtoList();
        }
        [Route("GetJobCoordinationStatus")]
        [HttpGet]
        public List<JobCoordinationStatusDto> GetJobCoordinationStatus()
        {
            return EnumerationsDtoManager.JobCoordinationStatusDtoList();
        }
        ////
        // GET: api/City/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/City
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/City/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/City/5
        public void Delete(int id)
        {
        }
    }
}
