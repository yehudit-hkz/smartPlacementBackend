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
            return CompanyDtoManager.CompanyDtoList();
        }
        [Route("GetByFilters")]
        public List<CompanyDto> Get([FromUri]CompanyFilters filters)
        {
            if(filters == null)
            {
                return Get();
            }
            return CompanyDtoManager.CompanyDtoListByFilters(filters);
        }

        [Route("GetById")]
        public CompanyDto Get(int id)
        {
            return CompanyDtoManager.CompanyDtoById(id);
        }

        [Route("Save")]
        public void Post([FromBody]CompanyDto companyDto)
        {
            CompanyDtoManager.NewCompanyDto(companyDto);
        }

        [Route("Edit")]
        public void Put([FromBody] CompanyDto companyDto)
        {
            CompanyDtoManager.CompanyDtoEditing(companyDto);
        }

        [Route("Delete")]
        public void Delete(int id)
        {
            CompanyDtoManager.DeleteCompanyDto(id);
        }
    }
}
