using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public static class JobsCoordinationDtoManager
    {
        public static List<CoordinatingJobsForGraduatesDto> JobsCoordinationDtoListByGraduate(string idGraduate)
        {
            return JobsCoordinationManager.JobsCoordinationListByGraduate(idGraduate);
        }
        public static List<CoordinatingJobsForGraduatesDto> JobsCoordinationDtoByJob(int IdJob)
        {
            return JobsCoordinationManager.JobsCoordinationListByJob(IdJob);
        }
        //public static void NewJobsCoordinationDto(CoordinatingJobsForGraduatesDto JobsCoordinationDto)
        //{
        //    JobsCoordinationManager.NewJobsCoordination(JobsCoordinationDto);
        //}
        public static void JobsCoordinationDtoEditing(CoordinatingJobsForGraduatesDto JobsCoordinationDto)
        {
            JobsCoordinationManager.JobsCoordinationEditing(JobsCoordinationDto);
        }
        //public static void DeleteJobsCoordinationDto(int id)
        //{
        //    JobsCoordinationManager.DeleteJobsCoordination(id);
        //}
    }
}
