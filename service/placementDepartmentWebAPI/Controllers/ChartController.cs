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
    public class ChartController : ApiController
    {
        // GET: api/Chart
        public List<ChartData> Get([FromUri]ChartsDetails chartsDetails)
        {
            try
            {
                return ChartBLManager.GetChart(chartsDetails);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        //// GET: api/Chart/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Chart
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Chart/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Chart/5
        //public void Delete(int id)
        //{
        //}
    }
}
