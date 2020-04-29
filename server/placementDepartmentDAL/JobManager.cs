using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using placementDepartmentCOMMON;

namespace placementDepartmentDAL
{
    public static class JobManager
    {
        public static ApiRes<JobDto> JobLazyList(JobFilters filters, string sort, int page ,int size)
        {
            ApiRes<JobDto> res = new ApiRes<JobDto>();
            sort = sort == " ," || sort == " , " ? "" : sort;
            filters.sendCV = (filters.sendCV == null) ? new List<bool>() : filters.sendCV;
            filters.active = (filters.active == null) ? new List<bool>() : filters.active;
            filters.subjects = (filters.subjects == null) ? new List<int>() : filters.subjects;
            DateTime dateMonthAgo = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                 res.items = placementDepartmentDB.Job
                      .Where(j =>
                        (!filters.sendCV.Any() || filters.sendCV.Contains(j.didSendCV)) &&
                        (!filters.active.Any() || filters.active.Contains(j.isActive)) &&
                        (filters.period == 0 ||
                         filters.period == 1 && j.dateReceived >= dateMonthAgo ||
                         filters.period == 2 && j.dateReceived >= filters.startDate && j.dateReceived <= filters.endDate) &&
                        (!filters.subjects.Any() || filters.subjects.Contains(j.subjectId))
                    )
                    .OrderBy("dateReceived desc" + sort)
                    .Skip(page*size)
                    .Take(size) 
                    .ProjectTo<JobDto>(AutoMapperConfiguration.config)
                    .ToList();
                res.totalCount = placementDepartmentDB.Job.Count();
                return res;
            }
        }
        public static List<JobDto> JobList()
        {
            List<JobDto> JobDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                JobDtos = placementDepartmentDB.Job
                    .OrderByDescending(j=>j.dateReceived)
                    .ProjectTo<JobDto>(AutoMapperConfiguration.config)
                    .ToList();
                //.Skip(p-1*s).Take(s)
                return JobDtos;
            }
        }
        public static List<JobDto> JobListByFilters(JobFilters filters)
        {
            List<JobDto> JobDtos;
            filters.sendCV = (filters.sendCV == null) ? new List<bool>(): filters.sendCV;
            filters.active = (filters.active == null) ? new List<bool>(): filters.active;
            filters.subjects = (filters.subjects == null) ? new List<int>(): filters.subjects;
            DateTime dateMonthAgo = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                JobDtos = placementDepartmentDB.Job
                    .Where(j =>
                        (!filters.sendCV.Any() || filters.sendCV.Contains(j.didSendCV)) &&
                        (!filters.active.Any() || filters.active.Contains(j.isActive)) &&
                        (filters.period == 0 ||
                         filters.period == 1 && j.dateReceived >= dateMonthAgo ||
                         filters.period == 2 && j.dateReceived >= filters.startDate && j.dateReceived <= filters.endDate) &&
                        (!filters.subjects.Any() || filters.subjects.Contains(j.subjectId))
                    )
                    .ProjectTo<JobDto>(AutoMapperConfiguration.config)
                    .ToList();
               return JobDtos;
            }
        }
        
        public static JobDto JobById(int Id)
        {
            Job Ret;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Ret = placementDepartmentDB.Job.Find(Id);
                return AutoMapperConfiguration.mapper.Map<JobDto>(Ret);
            }
        }
        public static void NewJob(JobDto JobDto)
        {
            Job Job = AutoMapperConfiguration.mapper.Map<Job>(JobDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Job.Add(Job);
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void JobEditing(JobDto JobDto)
        {
            Job Job = AutoMapperConfiguration.mapper.Map<Job>(JobDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Job.Attach(Job);
                placementDepartmentDB.Entry(Job).State = EntityState.Modified;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteJob(int id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Job Job = placementDepartmentDB.Job.Find(id);
                placementDepartmentDB.Job.Remove(Job);
                placementDepartmentDB.SaveChanges();
            }
        }
    }
}
