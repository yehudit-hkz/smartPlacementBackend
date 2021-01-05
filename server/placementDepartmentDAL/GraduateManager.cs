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
    public static class GraduateManager
    {
        public static ApiRes<MainGraduateDto> GraduateLazyList(GraduateFilters filters, string sort, int page, int size)
        {
            ApiRes<MainGraduateDto> res = new ApiRes<MainGraduateDto>();
            sort = sort == " ," || sort == " , " ? "" : sort;
            filters.active = filters.active ?? new List<bool>();
            filters.gender = filters.gender ?? new List<string>();
            filters.didGraduate = filters.didGraduate ?? new List<bool>();
            filters.hasDiploma = filters.hasDiploma ?? new List<bool>();
            filters.isWork = filters.isWork ?? new List<bool>();
            filters.expertise = filters.expertise ?? new List<int>();
            filters.branch = filters.branch ?? new List<int>();
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                res.items = placementDepartmentDB.Graduate
                    .Where(g =>
                   (g.firstName + " " + g.lastName).IndexOf(filters.name) != -1 &&
                        (!filters.active.Any() || filters.active.Contains(g.isActive)) &&
                        (!filters.gender.Any() || filters.gender.Contains(g.gender)) &&
                        (!filters.didGraduate.Any() || filters.didGraduate.Contains(g.didGraduate)) &&
                        (!filters.hasDiploma.Any() || filters.hasDiploma.Contains(g.hasDiploma)) &&
                        (!filters.isWork.Any() || filters.isWork.Contains(g.isWorkerInProfession)) &&
                        (filters.period == 0 ||
                        filters.period == 1 && g.dateOfRegistration == DateTime.Now ||
                        filters.period == 2 && g.dateOfRegistration >= filters.startDate && g.dateOfRegistration <= filters.endDate) &&
                        (!filters.expertise.Any() || filters.expertise.Contains(g.expertiseId)) &&
                        (!filters.branch.Any() || filters.branch.Contains(g.branchId))
                    )
                    .OrderBy(sort + "dateOfRegistration desc")
                    .Skip(page * size)
                    .Take(size)
                    .Select(g => new MainGraduateDto()
                    {
                        Id = g.Id,
                        Name = g.firstName + " " + g.lastName,
                        branchName = g.Branch.name,
                        expertiseName = g.Expertise.name,
                        endYear = g.endYear,
                        isActive = g.isActive
                    }).ToList();
                res.totalCount = placementDepartmentDB.Graduate.Where(g =>
                    (g.firstName + " " + g.lastName).IndexOf(filters.name) != -1 &&
                         (!filters.active.Any() || filters.active.Contains(g.isActive)) &&
                         (!filters.gender.Any() || filters.gender.Contains(g.gender)) &&
                        (!filters.didGraduate.Any() || filters.didGraduate.Contains(g.didGraduate)) &&
                        (!filters.hasDiploma.Any() || filters.hasDiploma.Contains(g.hasDiploma)) &&
                        (!filters.isWork.Any() || filters.isWork.Contains(g.isWorkerInProfession)) &&
                         (filters.period == 0 ||
                          filters.period == 1 && g.dateOfRegistration == DateTime.Now ||
                          filters.period == 2 && g.dateOfRegistration >= filters.startDate && g.dateOfRegistration <= filters.endDate) &&
                         (!filters.expertise.Any() || filters.expertise.Contains(g.expertiseId)) &&
                         (!filters.branch.Any() || filters.branch.Contains(g.branchId))
                    ).Count();
                return res;
            }
        }
        public static List<MainGraduateDto> GraduateList()
        {
            List<MainGraduateDto> graduateDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                graduateDtos = placementDepartmentDB.Graduate
                    .ProjectTo<MainGraduateDto>(AutoMapperConfiguration.config)
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
        public static List<FullGraduateDto> GraduateListById(List<string> Ids)
        {
            List<FullGraduateDto> Ret;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Ret = placementDepartmentDB.Graduate
                          .Where(g => Ids.Any(id => id == g.Id))
                          .ProjectTo<FullGraduateDto>(AutoMapperConfiguration.config)
                          .ToList();
                return Ret;
            }
        }
        public static List<FullGraduateDto> GraduateForJob(int idSubject, int idJob)
        {
            List<FullGraduateDto> graduateDtos;
            List<int> ExpertisesBySubject;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                ExpertisesBySubject = placementDepartmentDB.Subject.Find(idSubject)
                    .Expertise.Select(ex => ex.Id).ToList();
                graduateDtos = placementDepartmentDB.Graduate
                    .Where(g => ExpertisesBySubject.Contains(g.expertiseId)
                            && g.isActive == true
                            && !g.CoordinatingJobsForGraduates.Any(cd => cd.jobId == idJob))
                    .ProjectTo<FullGraduateDto>(AutoMapperConfiguration.config)
                    .ToList();
                return graduateDtos;
            }
        }
        public static FullGraduateDto NewGraduate(FullGraduateDto graduateDto)
        {
            Graduate graduate = AutoMapperConfiguration.mapper.Map<Graduate>(graduateDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                try
                {
                    graduate.dateOfRegistration = graduate.lastUpdate = DateTime.Now;
                    var res = placementDepartmentDB.Graduate.Add(graduate);
                    placementDepartmentDB.SaveChanges();
                    return AutoMapperConfiguration.mapper.Map<FullGraduateDto>(res);
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException != null)
                    {
                        if (sqlException.Number == 2627)
                        {
                            throw new Exception("Duplicate");
                        }
                    }
                    return null;
                }
                
            }
        }

        public static void GraduateEditing(FullGraduateDto graduateDto)
        {
            ReGraduateLanguages(graduateDto.Id, graduateDto.Languages);
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
            Graduate graduate = new Graduate() { Id = graduateId, linkToCV = filePath, lastUpdate = DateTime.Now };
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Configuration.ValidateOnSaveEnabled = false;
                placementDepartmentDB.Graduate.Attach(graduate);
                placementDepartmentDB.Entry(graduate).Property(x => x.linkToCV).IsModified = true;
                placementDepartmentDB.Entry(graduate).Property(x => x.lastUpdate).IsModified = true;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void ReGraduateLanguages(string graduateId, List<GraduateLanguagesDto> languages)
        {
            List<GraduateLanguages> graduateLanguages = AutoMapperConfiguration.mapper.Map<List<GraduateLanguages>>(languages);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.GraduateLanguages.RemoveRange(
                    placementDepartmentDB.GraduateLanguages.Where(gl =>
                        gl.graduateId == graduateId
                        ).ToList()
                    );
                placementDepartmentDB.GraduateLanguages.AddRange(graduateLanguages);
                placementDepartmentDB.SaveChanges();
            }

        }
        public static void GraduateEditingtrue(string id, bool isint)
        {
            Graduate graduate = new Graduate() { Id = id, isActive = isint, lastUpdate = DateTime.Now };
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
                try
                {
                    Graduate graduate = placementDepartmentDB.Graduate.Find(id);
                    placementDepartmentDB.Graduate.Remove(graduate);
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
