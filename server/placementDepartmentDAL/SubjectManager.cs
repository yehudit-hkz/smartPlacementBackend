using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using placementDepartmentCOMMON;

namespace placementDepartmentDAL
{
    public static class SubjectManager
    {
        public static List<SubjectDto> SubjectList()
        {
            List<SubjectDto> SubjectDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                SubjectDtos = placementDepartmentDB.Subject
                   .ProjectTo<SubjectDto>(AutoMapperConfiguration.config)
                   .ToList();
                return SubjectDtos;
            }
        }
        public static void NewSubject(SubjectDto SubjectDto)
        {
            Subject Subject = AutoMapperConfiguration.mapper.Map<Subject>(SubjectDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Subject.Add(Subject);
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteSubject(int id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Subject Subject = placementDepartmentDB.Subject.Find(id);
                placementDepartmentDB.Subject.Remove(Subject);
                placementDepartmentDB.SaveChanges();
            }
        }
    }
}
