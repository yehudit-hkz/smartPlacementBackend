using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;

namespace placementDepartmentDAL
{
    public class ChartManager
    {
        public static List<ChartData> GetJobsOpenedChart(DateTime start, DateTime end, int userid, int curUserid)
        {
            List<ChartData> res = new List<ChartData>
            {
                new ChartData("משרות שנפתחו בין " + start.ToShortDateString() + " לבין " + end.ToShortDateString())
            };
            bool isManagar = false;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                User cuser = placementDepartmentDB.User.Find(curUserid);
                if (cuser.permissionId == 1 && curUserid == userid)
                    isManagar = true;
                if (cuser.permissionId == 2 && curUserid != userid)
                    userid = curUserid;
                res[0].data = placementDepartmentDB.Subject
                 .Select(s => s.Job
                    .Where(j => 
                    (isManagar || j.handlesId == userid) &&
                    j.dateReceived >= start && j.dateReceived <= end)
                    .Count())
                 .ToList();
            }
            return res;
        }
        public static List<ChartData> GetPlacementsChart(DateTime start, DateTime end, int userid, int curUserid)
        {
            List<ChartData> res = new List<ChartData>
            {
                new ChartData("השמות בין " + start.ToShortDateString() + " לבין " + end.ToShortDateString())
            };
            bool isManagar = false;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                User cuser = placementDepartmentDB.User.Find(curUserid);
                if (cuser.permissionId == 1 && curUserid == userid)
                    isManagar = true;
                if (cuser.permissionId == 2 && curUserid != userid)
                    userid = curUserid;
                res[0].data = placementDepartmentDB.Subject
                 .Select(s => s.Job
                    .Where(j =>
                    (isManagar || j.handlesId == userid) &&
                    j.isActive==false&&j.ReasonForClosingThePosition.description== "בוצעה השמה" &&
                    j.lastUpdateDate >= start && j.lastUpdateDate <= end)
                    .Count())
                 .ToList();
            }
            return res;
        }
        public static List<ChartData> GetGraduatesVSJobs(List<int> branches,List<string> areas, int userid, int curUserid)
        {
            List<ChartData> res = new List<ChartData>
            {
                new ChartData("בוגרים זמינים"),
                new ChartData("משרות פתוחות")
            };
            branches = branches ?? new List<int>();
            areas = areas ?? new List<string>();
            bool isManagar = false;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                User cuser = placementDepartmentDB.User.Find(curUserid);
                if (cuser.permissionId == 1 && curUserid == userid)
                    isManagar = true;
                if (cuser.permissionId == 2 && curUserid != userid)
                    userid = curUserid;
                res[0].data = placementDepartmentDB.Subject
                 .Select(s => s.Expertise.Sum(e=> (int?)e.Graduate
                    .Where(g =>
                    g.isActive == true &&
                    (!branches.Any() || branches.Contains(g.branchId)) &&
                    (!areas.Any() || areas.Contains(g.City.area)))
                    .ToList().Count ) ?? 0)
                 .ToList();
                res[1].data = placementDepartmentDB.Subject
                 .Select(s => s.Job
                    .Where(j =>
                    ( isManagar || j.handlesId == userid) &&
                    j.isActive == true &&
                    !areas.Any() || areas.Contains(j.Contact.Company.City.area))
                    .Count())
                 .ToList();
            }
            return res;
        }

    }
}
