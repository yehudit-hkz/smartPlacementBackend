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
            try
            {
                return EnumerationsDtoManager.CityDtoList();
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        [Route("GetLanguages")]
        [HttpGet]
        public List<LanguageDto> GetLanguages()
        {
            try
            {
                return EnumerationsDtoManager.LanguageDtoList();
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        [Route("GetReasonForClosing")]
        [HttpGet]
        public List<ReasonForClosingThePositionDto> GetReasonForClosing()
        {
            try
            {
                return EnumerationsDtoManager.ReasonForClosingDtoList();
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        [Route("GetJobCoordinationStatus")]
        [HttpGet]
        public List<JobCoordinationStatusDto> GetJobCoordinationStatus()
        {
            try
            {
                return EnumerationsDtoManager.JobCoordinationStatusDtoList();
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
