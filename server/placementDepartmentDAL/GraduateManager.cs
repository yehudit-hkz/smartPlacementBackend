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
        public static ApiRes<FullGraduateDto> GraduateList(GraduateFilters filters, string sort, int page, int size)
        {
            ApiRes<FullGraduateDto> res = new ApiRes<FullGraduateDto>();
            sort = sort == " ," || sort == " , " ? "" : sort;
            filters.active = (filters.active == null) ? new List<bool>() : filters.active;
            filters.gender = (filters.gender == null) ? new List<string>() : filters.gender;
            filters.expertise = (filters.expertise == null) ? new List<int>() : filters.expertise;
            filters.branch = (filters.branch == null) ? new List<int>() : filters.branch;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                res.items = placementDepartmentDB.Graduate
                    .Where(g=>
                   $"{g.firstName} {g.lastName}".IndexOf(filters.name)!=-1&&
                        (!filters.active.Any() || filters.active.Contains(g.isActive)) &&
                        (!filters.gender.Any() || filters.gender.Contains(g.gender)) &&
                        (!filters.expertise.Any() || filters.expertise.Contains(g.expertiseId)) &&
                        (!filters.branch.Any() || filters.branch.Contains(g.branchId)) 
                    )
                    .OrderBy("dateOfRegistration desc" + sort)
                    .Skip(page * size)
                    .Take(size)
                    .ProjectTo<FullGraduateDto>(AutoMapperConfiguration.config)
                    .ToList();
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
                //.Skip(p-1*s).Take(s)
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
        public static List<FullGraduateDto> GraduateBySubject(int idSubject)
        {
            Subject subject;
            List<FullGraduateDto> graduateDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                subject = placementDepartmentDB.Subject.Find(idSubject);
                graduateDtos = placementDepartmentDB.Graduate
                    .Where(g => subject.Expertise.Contains(g.Expertise))//eq by ref or id?
                    .ProjectTo<FullGraduateDto>(AutoMapperConfiguration.config).ToList();
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
        public static void GraduateEditingtrue(string id,bool isint)
        {
            Graduate graduate = new Graduate() { Id = id ,isActive=isint};//AutoMapperConfiguration.mapper.Map<Graduate>(graduateDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Configuration.ValidateOnSaveEnabled = false;
                //graduate.lastUpdate = DateTime.Now;
                placementDepartmentDB.Graduate.Attach(graduate);
                placementDepartmentDB.Entry(graduate).Property(x => x.isActive).IsModified = true;
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
