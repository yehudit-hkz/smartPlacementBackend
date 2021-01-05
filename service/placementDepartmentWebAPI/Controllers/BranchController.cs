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
            return BranchDtoManager.BranchDtoList();
        }

        [Route("Save")]
        public int Post([FromBody]BranchDto branchDto)
        {
            return BranchDtoManager.newDtoBranch(branchDto);
        }
        [Route("Edit")]
        public void Put([FromBody]BranchDto branchDto)
        {
            BranchDtoManager.editDtoBranch(branchDto);
        }
        [Route("Delete")]
        public void Delete(int id)
        {
            BranchDtoManager.DeleteDtoBranch(id);
        }
    }
}
