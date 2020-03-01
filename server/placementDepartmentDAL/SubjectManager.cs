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
            List<SubjectDto> subjectDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                subjectDtos = placementDepartmentDB.subject
                   .ProjectTo<SubjectDto>(AutoMapperConfiguration.config)
                   .ToList();
                return subjectDtos;
            }
        }
        public static void NewSubject(SubjectDto subjectDto)
        {
            subject subject = AutoMapperConfiguration.mapper.Map<subject>(subjectDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.subject.Add(subject);
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteSubject(int id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                subject Subject = placementDepartmentDB.subject.Find(id);
                placementDepartmentDB.subject.Remove(Subject);
                placementDepartmentDB.SaveChanges();
            }
        }
    }
}
