using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using placementDepartmentCOMMON;
using placementDepartmentBLL;
using System.Web;

namespace placementDepartmentWebAPI.Controllers
{
    [RoutePrefix("api/ExprtWithSubj")]
    public class ExprtWithSubjController : ApiController
    {
        [Route("GetAll")]
        public List<ExpertiseWithSubjectDto> Get()
        {
            return EwithSDtoManager.SwithEDtoList();
        }
        [Route("Save")]
        public void Post([FromBody]ExpertiseWithSubjectDto expertiseWithSubject)
        {
            EwithSDtoManager.newDtoSubject(
                expertiseWithSubject.expertise.Id, expertiseWithSubject.subject
                );
        }
        [Route("Delete")]
        public void Delete(int idExpertise, int idSubject)
        {
            EwithSDtoManager.DeleteDtoSubject(idExpertise, idSubject);
        }
    }
}
