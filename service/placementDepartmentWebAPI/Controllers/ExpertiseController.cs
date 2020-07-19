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
    [RoutePrefix("api/Expertise")]
    public class ExpertiseController : ApiController
    {
        [Route("GetAll")]
        public List<ExpertiseDto> Get()
        {
            try
            {
                return ExpertiseDtoManager.ExpertiseDtoList();
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        // GET: api/Expertise/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [Route("Save")]
        public void Post([FromBody]ExpertiseDto expertiseDto)
        {
            try
            {
                ExpertiseDtoManager.newDtoExpertise(expertiseDto);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Expertise/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        [Route("Delete")]
        public void Delete(int id)
        {
            try
            {
                ExpertiseDtoManager.DeleteDtoExpertise(id);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
