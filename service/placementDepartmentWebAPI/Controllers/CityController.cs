using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using placementDepartmentBLL;
using placementDepartmentCOMMON;
using placementDepartmentWebAPI.util;

namespace placementDepartmentWebAPI.Controllers
{
    [RoutePrefix("api/City")]
    public class CityController : ApiController
    {
        [Route("GetAll")]
        public List<CityDto> Get()
        {
            return CityDtoManager.CityDtoList();
        }

        [Route("Save")]
        public int Post([FromBody]CityDto CityDto)
        {
            return CityDtoManager.newDtoCity(CityDto);
        }

        [Route("Edit")]
        public void Put([FromBody]CityDto CityDto)
        {
            CityDtoManager.editDtoCity(CityDto);
        }

        [Route("Delete")]
        public void Delete(int id)
        {
            CityDtoManager.DeleteDtoCity(id);
        }
    }
}
