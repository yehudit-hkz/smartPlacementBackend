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
    [RoutePrefix("api/Job")]
    public class JobController : ApiController
    {
        [Route("GetLazyList")]
        public ApiRes<JobDto> Get(int page, int size, string sort, [FromUri]JobFilters filters)
        {
            filters.curUserId = Int32.Parse(HttpContext.Current.User.Identity.Name);
            return JobDtoManager.JobDtoLazyList(filters, sort, page, size);
        }
        [Route("GetAll")]
        public List<JobDto> Get()
        {
            return JobDtoManager.JobDtoList();
        }
        [Route("GetByFilters")]
        public List<JobDto> Get([FromUri]JobFilters filters)
        {
            if (filters == null)
            {
                return Get();
            }
            return JobDtoManager.JobDtoListByFilters(filters);
        }
        [Route("GetById")]
        public JobDto Get(int id)
        {
            return JobDtoManager.JobDtoById(id);
        }
        [Route("Save")]
        public int Post([FromBody]JobDto jobDto)
        {
            return JobDtoManager.NewJobDto(jobDto);
        }
        [Route("Edit")]
        public void Put([FromBody]JobDto jobDto)
        {
            JobDtoManager.JobDtoEditing(jobDto);
        }
        [Route("Delete")]
        public void Delete(int id)
        {
            JobDtoManager.DeleteJobDto(id);
        }
    }
}
