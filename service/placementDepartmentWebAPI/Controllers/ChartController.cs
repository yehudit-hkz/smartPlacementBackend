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
    public class ChartController : ApiController
    {
        // GET: api/Chart
        public List<ChartData> Get([FromUri]ChartsDetails chartsDetails)
        {
            chartsDetails.curUserid = Int32.Parse(HttpContext.Current.User.Identity.Name);
            return ChartBLManager.GetChart(chartsDetails);
        }
    }
}
