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
        
        [Route("GetLazyList")]
        public ApiRes<JobDto> Get(int page, int size, string sort, [FromUri]JobFilters filters)
        {
            try
            {
                return JobDtoManager.JobDtoLazyList(filters, sort, page, size);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        [Route("GetAll")]   
        public List<JobDto> Get()
        {
            try
            {
                return JobDtoManager.JobDtoList();
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }   
        [Route("GetByFilters")]
        public List<JobDto> Get([FromUri]JobFilters filters)
        {
            try
            {
                if (filters == null)
                {
                    return Get();
                }
                return JobDtoManager.JobDtoListByFilters(filters);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        [Route("GetById")]
        public JobDto Get(int id)
        {
            try
            {
                return JobDtoManager.JobDtoById(id);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("Save")]
        public int Post([FromBody]JobDto jobDto)
        {
            try
            {
                return JobDtoManager.NewJobDto(jobDto);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("Edit")]
        public void Put([FromBody]JobDto jobDto)
        {
            try
            {
                JobDtoManager.JobDtoEditing(jobDto);
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
                JobDtoManager.DeleteJobDto(id);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
