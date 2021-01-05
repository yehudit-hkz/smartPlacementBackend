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
    [RoutePrefix("api/Contact")]
    public class ContactController : ApiController
    {
        [Route("GetByCompany")]
        public List<ContactDto> GetByCompany(int idCompany)
        {
            return ContactDtoManager.ContactDtoListByCompany(idCompany);
        }

        [Route("GetById")]
        public ContactDto GetById(int id)
        {
            return ContactDtoManager.ContactDtoById(id);
        }

        [Route("Save")]
        public void Post([FromBody]ContactDto contactDto)
        {
            ContactDtoManager.NewContactDto(contactDto);
        }

        [Route("Edit")]
        public void Put([FromBody]ContactDto contactDto)
        {
            ContactDtoManager.ContactDtoEditing(contactDto);
        }

        [Route("Delete")]
        public void Delete(int id)
        {
            ContactDtoManager.DeleteContactDto(id);
        }
    }
}
