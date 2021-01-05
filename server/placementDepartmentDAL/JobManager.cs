using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
        public static ApiRes<JobDto> JobLazyList(JobFilters filters, string sort, int page, int size)
        {
            ApiRes<JobDto> res = new ApiRes<JobDto>();
            sort = sort == " ," || sort == " , " ? "" : sort;
            filters.sendCV = filters.sendCV ?? new List<bool>();
            filters.active = filters.active ?? new List<bool>();
            filters.ReasonClosing = filters.ReasonClosing ?? new List<int>();
            filters.subjects = filters.subjects ?? new List<int>();
            filters.user = filters.user ?? new List<int>();
            DateTime dateMonthAgo = DateTime.Now.AddMonths(-1);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                User cuser = placementDepartmentDB.User.Find(filters.curUserId);
                if (cuser.permissionId == 2)
                    filters.user = new List<int>(1) { cuser.Id };
                res.items = placementDepartmentDB.Job
                     .Where(j =>
                       ((cuser.permissionId == 1 && !filters.user.Any()) || filters.user.Contains(j.handlesId)) &&
                       (!filters.sendCV.Any() || filters.sendCV.Contains(j.didSendCV)) &&
                       (!filters.active.Any() || filters.active.Contains(j.isActive)) &&
                       (filters.period == 0 ||
                        filters.period == 1 && j.dateReceived >= dateMonthAgo ||
                        filters.period == 2 && j.dateReceived >= filters.startDate && j.dateReceived <= filters.endDate) &&
                       (!filters.ReasonClosing.Any() || filters.ReasonClosing.Contains(j.reasonForClosing.Value)) &&
                       (!filters.subjects.Any() || filters.subjects.Contains(j.subjectId))
                   )
                   .OrderBy(sort + "dateReceived desc")
                   .Skip(page * size)
                   .Take(size)
                   .ProjectTo<JobDto>(AutoMapperConfiguration.config)
                   .ToList();
                res.totalCount = placementDepartmentDB.Job.Where(j =>
                        ((cuser.permissionId == 1 && !filters.user.Any()) || filters.user.Contains(j.handlesId)) &&
                        (!filters.sendCV.Any() || filters.sendCV.Contains(j.didSendCV)) &&
                        (!filters.active.Any() || filters.active.Contains(j.isActive)) &&
                        (filters.period == 0 ||
                         filters.period == 1 && j.dateReceived >= dateMonthAgo ||
                         filters.period == 2 && j.dateReceived >= filters.startDate && j.dateReceived <= filters.endDate) &&
                        (!filters.subjects.Any() || filters.subjects.Contains(j.subjectId))
                    ).Count();
                return res;
            }
        }
        public static List<JobDto> JobList()
        {
            List<JobDto> JobDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                JobDtos = placementDepartmentDB.Job
                    .OrderByDescending(j => j.dateReceived)
                    .ProjectTo<JobDto>(AutoMapperConfiguration.config)
                    .ToList();
                return JobDtos;
            }
        }
        public static List<JobDto> JobListByFilters(JobFilters filters)
        {
            List<JobDto> JobDtos;
            filters.sendCV = filters.sendCV ?? new List<bool>();
            filters.active = filters.active ?? new List<bool>();
            filters.subjects = filters.subjects ?? new List<int>();
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

        public static List<string> get0TitleAnd1ContactMailOfJobById(int Id)
        {
            List<string> detailes = new List<string>();
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                detailes.Add(placementDepartmentDB.Job.Find(Id).title);
                detailes.Add(placementDepartmentDB.Job.Find(Id).Contact.email);
                return detailes;
            }
        }

        public static int NewJob(JobDto JobDto)
        {
            int newJobId;
            Job Job = AutoMapperConfiguration.mapper.Map<Job>(JobDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Configuration.ValidateOnSaveEnabled = false;
                Job.dateReceived = Job.lastUpdateDate = DateTime.Now;
                Job = placementDepartmentDB.Job.Add(Job);
                placementDepartmentDB.SaveChanges();
                newJobId = Job.Id;
            }
            return newJobId;
        }
        public static void JobEditing(JobDto JobDto)
        {
            Job Job = AutoMapperConfiguration.mapper.Map<Job>(JobDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Job.lastUpdateDate = DateTime.Now;
                placementDepartmentDB.Job.Attach(Job);
                placementDepartmentDB.Entry(Job).State = EntityState.Modified;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void JobUpdate(int idJob, bool didSendCV)
        {
            Job job = new Job() { Id = idJob, didSendCV = didSendCV, lastUpdateDate = DateTime.Now };
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Configuration.ValidateOnSaveEnabled = false;
                placementDepartmentDB.Job.Attach(job);
                placementDepartmentDB.Entry(job).Property(x => x.didSendCV).IsModified = true;
                placementDepartmentDB.Entry(job).Property(x => x.lastUpdateDate).IsModified = true;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteJob(int id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                try
                {
                    Job Job = placementDepartmentDB.Job.Find(id);
                    placementDepartmentDB.Job.Remove(Job);
                    placementDepartmentDB.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException != null)
                    {
                        if (sqlException.Number == 547)
                        {
                            throw new DbUpdateException("547");
                        }
                    }
                }
            }
        }
    }
}
