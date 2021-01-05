using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using placementDepartmentBLL;
using placementDepartmentCOMMON;

namespace placementDepartmentWebAPI.Controllers
{
    [RoutePrefix("api/Language")]
    public class LanguageController : ApiController
    {
        [Route("GetAll")]
        public List<LanguageDto> Get()
        {
            return LanguageDtoManager.LanguageDtoList();
        }

        [Route("Save")]
        public int Post([FromBody]LanguageDto LanguageDto)
        {
            return LanguageDtoManager.newDtoLanguage(LanguageDto);
        }

        [Route("Edit")]
        public void Put([FromBody]LanguageDto LanguageDto)
        {
            LanguageDtoManager.editDtoLanguage(LanguageDto);
        }

        [Route("Delete")]
        public void Delete(int id)
        {
            LanguageDtoManager.DeleteDtoLanguage(id);
        }
    }
}
