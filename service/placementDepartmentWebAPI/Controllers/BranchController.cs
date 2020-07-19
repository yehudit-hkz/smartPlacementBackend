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
    [RoutePrefix("api/Branch")]
    public class BranchController : ApiController
    {
        [Route("GetAll")]
        public List<BranchDto> Get()
        {
            try
            {
                return BranchDtoManager.BranchDtoList();
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        // GET: api/Branch/5
        //public string Get(int id)
        //{
        //    return "value";
        //}
        [Route("Save")]
        public void Post([FromBody]BranchDto branchDto)
        {
            try
            {
                BranchDtoManager.newDtoBranch(branchDto);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Branch/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}
        [Route("Delete")]
        public void Delete(int id)
        {
            try
            {
                BranchDtoManager.DeleteDtoBranch(id);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
