using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using placementDepartmentCOMMON;

namespace placementDepartmentDAL
{
    public static class GraduateManager
    {
        public static List<GraduateDto> GraduateList()
        {
            List<GraduateDto> graduateDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                graduateDtos = placementDepartmentDB.Graduate
                    .ProjectTo<GraduateDto>(AutoMapperConfiguration.config)
                    .ToList();
                //.Skip(p-1*s).Take(s)
                return graduateDtos;
            }
        }
        public static GraduateDto GraduateById(string Id)
        {
            Graduate Ret;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Ret = placementDepartmentDB.Graduate.Find(Id);
                return AutoMapperConfiguration.mapper.Map<GraduateDto>(Ret);
            }
        }
        public static List<GraduateDto> GraduateBySubject(SubjectDto subjectDto)
        {
            Subject subject = AutoMapperConfiguration.mapper.Map<Subject>(subjectDto);
            List<GraduateDto> graduateDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                graduateDtos = placementDepartmentDB.Graduate
                    .Where(g => subject.Expertise.Contains(g.Expertise))//eq by ref or id?
                    .ProjectTo<GraduateDto>(AutoMapperConfiguration.config).ToList();
                return graduateDtos;
            }
        }
        public static void NewGraduate(GraduateDto graduateDto)
        {
            Graduate graduate = AutoMapperConfiguration.mapper.Map<Graduate>(graduateDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                if (graduate.dateOfRegistration == null)
                    graduate.dateOfRegistration =DateTime.Now; 
                graduate.lastUpdate = DateTime.Now;
                placementDepartmentDB.Graduate.Add(graduate);
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void GraduateEditing(GraduateDto graduateDto)
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
