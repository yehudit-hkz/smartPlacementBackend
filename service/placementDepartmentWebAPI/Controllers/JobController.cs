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
    [RoutePrefix("api/Job")]
    public class JobController : ApiController
    {
        
        [Route("GetLazy")]
        public ApiRes<JobDto> Get(string sort, int page, int size, [FromUri]JobFilters filters)
        {
            return JobDtoManager.JobDtoLazyList(filters,sort,page,size);
        }
        [Route("GetAll")]   
        public List<JobDto> Get()
        {
            return JobDtoManager.JobDtoList();
        }   
        [Route("GetByFilters")]
        public List<JobDto> Get([FromUri]JobFilters filters)
        {
            if (filters==null)
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
        public void Post([FromBody]JobDto jobDto)
        {
            JobDtoManager.NewJobDto(jobDto);
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
