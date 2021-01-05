using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
        public static int NewSubject(SubjectDto SubjectDto)
        {
            Subject Subject = AutoMapperConfiguration.mapper.Map<Subject>(SubjectDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Subject = placementDepartmentDB.Subject.Add(Subject);
                placementDepartmentDB.SaveChanges();
                return Subject.Id;
            }
        }
        public static void editSubject(SubjectDto SubjectDto)
        {
            Subject Subject = AutoMapperConfiguration.mapper.Map<Subject>(SubjectDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Subject.Attach(Subject);
                placementDepartmentDB.Entry(Subject).State = EntityState.Modified;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteSubject(int id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                try
                {
                    Subject Subject = placementDepartmentDB.Subject.Find(id);
                    placementDepartmentDB.Subject.Remove(Subject);
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
