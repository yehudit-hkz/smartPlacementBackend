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

        [Route("Save")]
        public int Post([FromBody]SubjectDto subjectDto)
        {
                return SubjectDtoManager.newDtoSubject(subjectDto);
        }

        [Route("Edit")]
        public void Put([FromBody]SubjectDto subjectDto)
        {
                SubjectDtoManager.editDtoSubject(subjectDto);
        }

        [Route("Delete")]
        public void Delete(int id)
        {
                SubjectDtoManager.DeleteDtoSubject(id);
        }
    }
}
