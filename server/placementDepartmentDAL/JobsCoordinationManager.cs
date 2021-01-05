using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using placementDepartmentCOMMON;

namespace placementDepartmentDAL
{
    public static class JobsCoordinationManager
    {
        public static ApiRes<CoordinatingJobsForGraduatesDto> JobCoordinationLazyList(JobCoordinationFilters filters, string sort, int page, int size)
        {
            ApiRes<CoordinatingJobsForGraduatesDto> res = new ApiRes<CoordinatingJobsForGraduatesDto>();
            sort = sort == " ," || sort == " , " ? "" : sort;
            filters.status = filters.status ?? new List<int>();
            filters.branch = filters.branch ?? new List<int>();
            filters.gender = filters.gender ?? new List<string>();
            filters.subject = filters.subject ?? new List<int>();
            filters.user = filters.user ?? new List<int>();
            DateTime dateMonthAgo = DateTime.Now.AddMonths(-1);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                User cuser = placementDepartmentDB.User.Find(filters.curUserId);
                if (cuser.permissionId == 2)
                    filters.user = new List<int>(1) { cuser.Id };
                res.items = placementDepartmentDB.CoordinatingJobsForGraduates
                     .Where(cj =>
                       ((cuser.permissionId == 1 && !filters.user.Any()) || filters.user.Contains(cj.Job.handlesId)) &&
                       (!filters.status.Any() || filters.status.Contains(cj.placementStatus)) &&
                       (!filters.branch.Any() || filters.branch.Contains(cj.Graduate.branchId)) &&
                       (!filters.gender.Any() || filters.gender.Contains(cj.Graduate.gender)) &&
                       (filters.period == 0 ||
                        filters.period == 1 && cj.dateReceived >= dateMonthAgo ||
                        filters.period == 2 && cj.dateReceived >= filters.startDate && cj.dateReceived <= filters.endDate) &&
                       (!filters.subject.Any() || filters.subject.Contains(cj.Job.subjectId))
                   )
                   .OrderBy(sort + "dateReceived desc")
                   .Skip(page * size)
                   .Take(size)
                   .ProjectTo<CoordinatingJobsForGraduatesDto>(AutoMapperConfiguration.config)
                   .ToList();
                res.totalCount = placementDepartmentDB.CoordinatingJobsForGraduates.Where(cj =>
                       ((cuser.permissionId == 1 && !filters.user.Any()) || filters.user.Contains(cj.Job.handlesId)) &&
                       (!filters.status.Any() || filters.status.Contains(cj.placementStatus)) &&
                       (!filters.branch.Any() || filters.branch.Contains(cj.Graduate.branchId)) &&
                       (!filters.gender.Any() || filters.gender.Contains(cj.Graduate.gender)) &&
                       (filters.period == 0 ||
                        filters.period == 1 && cj.dateReceived >= dateMonthAgo ||
                        filters.period == 2 && cj.dateReceived >= filters.startDate && cj.dateReceived <= filters.endDate) &&
                       (!filters.subject.Any() || filters.subject.Contains(cj.Job.subjectId))
                   ).Count();
                return res;
            }
        }
        public static List<CoordinatingJobsForGraduatesDto> JobsCoordinationListByGraduate(string idGraduate, int idUser)
        {
            List<CoordinatingJobsForGraduatesDto> JobsCoordinationDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                User user = placementDepartmentDB.User.Find(idUser);
                JobsCoordinationDtos = placementDepartmentDB.CoordinatingJobsForGraduates
                        .Where(cj => cj.candidateId == idGraduate &&
                              (user.permissionId == 1 || cj.Job.handlesId == idUser))
                        .ProjectTo<CoordinatingJobsForGraduatesDto>(AutoMapperConfiguration.config)
                        .ToList();
                return JobsCoordinationDtos;
            }
        }
        public static List<CoordinatingJobsForGraduatesDto> JobsCoordinationListByJob(int IdJob)
        {
            List<CoordinatingJobsForGraduatesDto> JobsCoordinationDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                JobsCoordinationDtos = placementDepartmentDB.CoordinatingJobsForGraduates
                    .Where(cj => cj.jobId == IdJob)
                    .ProjectTo<CoordinatingJobsForGraduatesDto>(AutoMapperConfiguration.config)
                    .ToList();
                return JobsCoordinationDtos;
            }
        }
        public static List<CoordinatingJobsForGraduates> NewJobsCoordination(List<CoordinatingJobsForGraduates> JobsCoordination)
        {
            List<CoordinatingJobsForGraduates> res;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                res = placementDepartmentDB.CoordinatingJobsForGraduates.AddRange(JobsCoordination).ToList();
                placementDepartmentDB.SaveChanges();
                return res;
            }
        }
        public static void JobsCoordinationUpdate(List<int> idCoordinatingJobsForGraduate, int statusId)
        {
            if (statusId == 6)//work
            {
                placedGraduateJob(idCoordinatingJobsForGraduate);
            }
            else
            {
                List<CoordinatingJobsForGraduates> coordinations = new List<CoordinatingJobsForGraduates>();
                foreach (var item in idCoordinatingJobsForGraduate)
                {
                    coordinations.Add(new CoordinatingJobsForGraduates()
                    {
                        Id = item,
                        placementStatus = statusId,
                        lastUpdateDate = DateTime.Now
                    });
                }
                using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
                {
                    foreach (var coordination in coordinations)
                    {
                        placementDepartmentDB.Configuration.ValidateOnSaveEnabled = false;
                        placementDepartmentDB.CoordinatingJobsForGraduates.Attach(coordination);
                        placementDepartmentDB.Entry(coordination).Property(x => x.placementStatus).IsModified = true;
                        placementDepartmentDB.Entry(coordination).Property(x => x.lastUpdateDate).IsModified = true;
                    }
                    placementDepartmentDB.SaveChanges();
                }
            }
        }
        public static void placedGraduateJob(List<int> idCRDs)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                foreach (var idCRD in idCRDs)
                {
                    CoordinatingJobsForGraduates coordinating = placementDepartmentDB.CoordinatingJobsForGraduates.Find(idCRD);
                    coordinating.placementStatus = 6;
                    coordinating.JobCoordinationStatus = null;
                    coordinating.lastUpdateDate = DateTime.Now;
                    //Job
                    coordinating.Job.isActive = false;
                    coordinating.Job.reasonForClosing = 5; //work
                    coordinating.Job.ReasonForClosingThePosition = null;
                    coordinating.Job.lastUpdateDate = DateTime.Now;
                    //Graduate
                    coordinating.Graduate.isWorkerInProfession = true;
                    coordinating.Graduate.placedByThePlacementDepartment = true;
                    coordinating.Graduate.lastUpdate = DateTime.Now;
                    coordinating.Graduate.companyName = coordinating.Job.Contact.Company.name;
                    coordinating.Graduate.role = coordinating.Job.Subject.name;
                    placementDepartmentDB.SaveChanges();
                }
            }
        }

        public static void DeleteJobsCoordination(List<int> ids)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                foreach (var id in ids)
                {
                    CoordinatingJobsForGraduates JobsCoordination = placementDepartmentDB.CoordinatingJobsForGraduates.Find(id);
                    placementDepartmentDB.CoordinatingJobsForGraduates.Remove(JobsCoordination);
                }
                placementDepartmentDB.SaveChanges();
            }
        }
    }
}
