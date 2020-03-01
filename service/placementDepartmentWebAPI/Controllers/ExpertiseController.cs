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
            return ExpertiseDtoManager.ExpertiseDtoList();
        }

        // GET: api/Expertise/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("Save")]
        public void Post([FromBody]ExpertiseDto expertiseDto)
        {
            ExpertiseDtoManager.newDtoExpertise(expertiseDto);
        }

        // PUT: api/Expertise/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [Route("Delete")]
        public void Delete(int id)
        {
            ExpertiseDtoManager.DeleteDtoExpertise(id);
        }
    }
}
