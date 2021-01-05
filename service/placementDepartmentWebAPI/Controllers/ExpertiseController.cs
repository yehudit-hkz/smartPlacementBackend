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

        [Route("Save")]
        public int Post([FromBody]ExpertiseDto expertiseDto)
        {
            return ExpertiseDtoManager.newDtoExpertise(expertiseDto);
        }

        [Route("Edit")]
        public void Put([FromBody]ExpertiseDto expertiseDto)
        {
            ExpertiseDtoManager.editDtoExpertise(expertiseDto);
        }

        [Route("Delete")]
        public void Delete(int id)
        {
            ExpertiseDtoManager.DeleteDtoExpertise(id);
        }
    }
}
