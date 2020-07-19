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
    [RoutePrefix("api/Company")]
    public class CompanyController : ApiController
    {
        [Route("GetAll")]
        public List<CompanyDto> Get()
        {
            try
            {
                return CompanyDtoManager.CompanyDtoList();
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        [Route("GetByFilters")]
        public List<CompanyDto> Get([FromUri]CompanyFilters filters)
        {
            try
            {
                if (filters == null)
                {
                    return Get();
                }
                return CompanyDtoManager.CompanyDtoListByFilters(filters);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("GetById")]
        public CompanyDto Get(int id)
        {
            try
            {
                return CompanyDtoManager.CompanyDtoById(id);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("Save")]
        public void Post([FromBody]CompanyDto companyDto)
        {
            try
            {
                CompanyDtoManager.NewCompanyDto(companyDto);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("Edit")]
        public void Put([FromBody] CompanyDto companyDto)
        {
            try
            {
                CompanyDtoManager.CompanyDtoEditing(companyDto);
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
                CompanyDtoManager.DeleteCompanyDto(id);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
