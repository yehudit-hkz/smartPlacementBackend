using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using placementDepartmentCOMMON;

namespace placementDepartmentDAL
{
   public static class EnumerationsManager
    {
        public static List<ReasonForClosingThePositionDto> ReasonForClosingList()
        {
            List<ReasonForClosingThePositionDto> reasonForClosingThePositionDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                reasonForClosingThePositionDtos = placementDepartmentDB.ReasonForClosingThePosition
                    .ProjectTo<ReasonForClosingThePositionDto>(AutoMapperConfiguration.config)
                    .ToList();
                return reasonForClosingThePositionDtos;
            }
        }
        public static List<JobCoordinationStatusDto> JobCoordinationStatusList()
        {
            List<JobCoordinationStatusDto> jobCoordinationStatusDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                jobCoordinationStatusDtos = placementDepartmentDB.JobCoordinationStatus
                    .ProjectTo<JobCoordinationStatusDto>(AutoMapperConfiguration.config)
                    .ToList();
                return jobCoordinationStatusDtos;
            }
        }
        public static List<PermissionDto> PermissionsList()
        {
            List<PermissionDto> permissionDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                permissionDtos = placementDepartmentDB.Permission
                    .ProjectTo<PermissionDto>(AutoMapperConfiguration.config)
                    .ToList();
                return permissionDtos;
            }
        }
    }
}
