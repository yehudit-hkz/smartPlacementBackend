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
            try
            {
                return ContactDtoManager.ContactDtoListByCompany(idCompany);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("GetById")]
        public ContactDto GetById(int id)
        {
            try
            {
                return ContactDtoManager.ContactDtoById(id);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("Save")]
        public void Post([FromBody]ContactDto contactDto)
        {
            try
            {
                ContactDtoManager.NewContactDto(contactDto);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("Edit")]
        public void Put([FromBody]ContactDto contactDto)
        {
            try
            {
                ContactDtoManager.ContactDtoEditing(contactDto);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("Delete")]
        public void Delete(int id)
        {
            try
            {
                ContactDtoManager.DeleteContactDto(id);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
