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
    [RoutePrefix("api/List")]

    public class EnumerationsController : ApiController
    {
        [Route("GetReasonForClosing")]
        [HttpGet]
        public List<ReasonForClosingThePositionDto> GetReasonForClosing()
        {
            return EnumerationsDtoManager.ReasonForClosingDtoList();
        }

        [Route("GetJobCoordinationStatus")]
        [HttpGet]
        public List<JobCoordinationStatusDto> GetJobCoordinationStatus()
        {
            return EnumerationsDtoManager.JobCoordinationStatusDtoList();
        }

        [Route("GetPermissions")]
        [HttpGet]
        public List<PermissionDto> GetPermissions()
        {
            return EnumerationsDtoManager.PermissionsDtoList();
        }

        [Route("GetPages")]
        [HttpGet]
        public System.Object GetPages()
        {
            var v = new[]
            {
                new { uri= "graduates" , name= "בוגרים" },
                new { uri= "jobs" ,      name= "משרות" },
                new { uri= "companies" , name= "חברות" },
                new { uri= "Placements", name= "השמות" },
                new { uri= "Charts" ,    name= "תרשימים" },

                new { uri= "users" , name= "משתשים" },
                new { uri= "lists" , name= "תחזוקה" }
            };
            var list = v.ToList();

            var userId = Int32.Parse(HttpContext.Current.User.Identity.Name);
            if (UserBLManager.UserDtoById(userId).Permission.Id != 1)
            {
                list.RemoveAt(6);
                list.RemoveAt(5);
            }

            return list;
        }
    }
}
