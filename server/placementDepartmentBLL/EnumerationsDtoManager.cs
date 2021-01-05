using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentDAL;
using placementDepartmentCOMMON;

namespace placementDepartmentBLL
{
    public static class EnumerationsDtoManager
    {
        public static List<ReasonForClosingThePositionDto> ReasonForClosingDtoList()
        {
            return EnumerationsManager.ReasonForClosingList();
        }
        public static List<JobCoordinationStatusDto> JobCoordinationStatusDtoList()
        {
            return EnumerationsManager.JobCoordinationStatusList();
        }
        public static List<PermissionDto> PermissionsDtoList()
        {
            return EnumerationsManager.PermissionsList();
        }
    }
}
