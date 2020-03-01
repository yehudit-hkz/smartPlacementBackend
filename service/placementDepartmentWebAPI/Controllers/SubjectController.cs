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
    [RoutePrefix("api/Subject")]
    public class SubjectController : ApiController
    {
        [Route("GetAll")]
        public List<SubjectDto> Get()
        {
            return SubjectDtoManager.SubjectDtoList();
        }

        // GET: api/Subject/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("Save")]
        public void Post([FromBody]SubjectDto subjectDto)
        {
            SubjectDtoManager.newDtoSubject(subjectDto);
        }

        // PUT: api/Subject/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [Route("Delete")]
        public void Delete(int id)
        {
            SubjectDtoManager.DeleteDtoSubject(id);
        }
    }
}
