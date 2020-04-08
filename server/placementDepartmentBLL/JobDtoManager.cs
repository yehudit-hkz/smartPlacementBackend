using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public static class JobDtoManager
    {
        public static List<JobDto> JobDtoList()
        {
            return JobManager.JobList();
        }
        public static List<JobDto> JobDtoListByFilters(JobFilters filters)
        {
            return JobManager.JobListByFilters(filters);
        }
        public static JobDto JobDtoById(int Id)
        {
            return JobManager.JobById(Id);
        }
        public static void NewJobDto(JobDto JobDto)
        {
            JobManager.NewJob(JobDto);
        }
        public static void JobDtoEditing(JobDto JobDto)
        {
            JobManager.JobEditing(JobDto);
        }
        public static void DeleteJobDto(int id)
        {
            JobManager.DeleteJob(id);
        }
    }
}
