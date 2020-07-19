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
    public static class GraduateManager
    {
        public static ApiRes<MainGraduateDto> GraduateLazyList(GraduateFilters filters, string sort, int page, int size)
        {
            ApiRes<MainGraduateDto> res = new ApiRes<MainGraduateDto>();
            sort = sort == " ," || sort == " , " ? "" : sort;
            filters.active = filters.active ?? new List<bool>(); 
            filters.gender = filters.gender ?? new List<string>();
            filters.expertise = filters.expertise ?? new List<int>();
            filters.branch = filters.branch ?? new List<int>();
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                res.items = placementDepartmentDB.Graduate
                    .Where(g =>
                   $"{g.firstName} {g.lastName}".IndexOf(filters.name) != -1 &&
                        (!filters.active.Any() || filters.active.Contains(g.isActive)) &&
                        (!filters.gender.Any() || filters.gender.Contains(g.gender)) &&
                        (!filters.expertise.Any() || filters.expertise.Contains(g.expertiseId)) &&
                        (!filters.branch.Any() || filters.branch.Contains(g.branchId))
                    )
                    .OrderBy("dateOfRegistration desc" + sort)
                    .Skip(page * size)
                    .Take(size)
                    .Select(g => new MainGraduateDto()
                    {
                        Id = g.Id,
                        Name = $"{g.firstName} {g.lastName}",
                        branchName = g.Branch.name,
                        expertiseName=g.Expertise.name, 
                        endYear=g.Expertise.name,
                        isActive=g.isActive
                    }).ToList();
                res.totalCount = placementDepartmentDB.Graduate.Where(g =>
                    $"{g.firstName} {g.lastName}".IndexOf(filters.name) != -1 &&
                         (!filters.active.Any() || filters.active.Contains(g.isActive)) &&
                         (!filters.gender.Any() || filters.gender.Contains(g.gender)) &&
                         (!filters.expertise.Any() || filters.expertise.Contains(g.expertiseId)) &&
                         (!filters.branch.Any() || filters.branch.Contains(g.branchId))
                    ).Count();
                return res;
            }
        }
        public static List<FullGraduateDto> GraduateList()
        {
            List<FullGraduateDto> graduateDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                graduateDtos = placementDepartmentDB.Graduate
                    .ProjectTo<FullGraduateDto>(AutoMapperConfiguration.config)
                    .ToList();
                return graduateDtos;
            }
        }
        public static FullGraduateDto GraduateById(string Id)
        {
            Graduate Ret;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Ret = placementDepartmentDB.Graduate.Find(Id);
                return AutoMapperConfiguration.mapper.Map<FullGraduateDto>(Ret);
            }
        }
        public static List<FullGraduateDto> GraduateForJob(int idSubject, int idJob)
        {
            List<FullGraduateDto> graduateDtos;
            List<int> ExpertisesBySubject;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                ExpertisesBySubject = placementDepartmentDB.Subject.Find(idSubject)
                    .Expertise.Select(ex=>ex.Id).ToList();
                graduateDtos = placementDepartmentDB.Graduate
                    .Where(g =>  ExpertisesBySubject.Contains(g.expertiseId)
                    && g.isActive==true
                    && !g.CoordinatingJobsForGraduates.Any(cd => cd.jobId == idJob))
                    .ProjectTo<FullGraduateDto>(AutoMapperConfiguration.config)
                    .ToList();
                return graduateDtos;
            }
        }
        public static void NewGraduate(FullGraduateDto graduateDto)
        {
            Graduate graduate = AutoMapperConfiguration.mapper.Map<Graduate>(graduateDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                if (graduate.dateOfRegistration == null)//for import in initialization
                    graduate.dateOfRegistration =DateTime.Now;
                if (graduate.lastUpdate == null)//for import in initialization
                    graduate.lastUpdate = DateTime.Now;

                placementDepartmentDB.Graduate.Add(graduate);
                placementDepartmentDB.SaveChanges();
            }
        }

        public static void GraduateEditing(FullGraduateDto graduateDto)
        {
            Graduate graduate = AutoMapperConfiguration.mapper.Map<Graduate>(graduateDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                graduate.lastUpdate = DateTime.Now;
                placementDepartmentDB.Graduate.Attach(graduate);
                placementDepartmentDB.Entry(graduate).State = EntityState.Modified;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void GraduateUploudFile(string graduateId, string filePath)
        {
            Graduate graduate = new Graduate() { Id = graduateId, linkToCV=filePath, lastUpdate = DateTime.Now };
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Configuration.ValidateOnSaveEnabled = false;
                placementDepartmentDB.Graduate.Attach(graduate);
                placementDepartmentDB.Entry(graduate).Property(x => x.linkToCV).IsModified = true;
                placementDepartmentDB.Entry(graduate).Property(x => x.lastUpdate).IsModified = true;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void GraduateEditingtrue(string id,bool isint)
        {
            Graduate graduate = new Graduate() { Id = id ,isActive=isint ,lastUpdate= DateTime.Now };
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Configuration.ValidateOnSaveEnabled = false;
                placementDepartmentDB.Graduate.Attach(graduate);
                placementDepartmentDB.Entry(graduate).Property(x => x.isActive).IsModified = true;
                placementDepartmentDB.Entry(graduate).Property(x => x.lastUpdate).IsModified = true;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteGraduate(string id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Graduate graduate = placementDepartmentDB.Graduate.Find(id);
                placementDepartmentDB.Graduate.Remove(graduate);
                placementDepartmentDB.SaveChanges();
            }
        }
    }
}
