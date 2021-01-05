using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace placementDepartmentDAL
{
    public static class ExpertiseManager
    {
        public static List<ExpertiseDto> ExpertiseList()
        {
            List<ExpertiseDto> expertiseDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                expertiseDtos = placementDepartmentDB.Expertise
                   .ProjectTo<ExpertiseDto>(AutoMapperConfiguration.config)
                   .ToList();
                return expertiseDtos;
            }
        }
        public static int NewExpertise(ExpertiseDto expertiseDto)
        {
            Expertise expertise = AutoMapperConfiguration.mapper.Map<Expertise>(expertiseDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                expertise = placementDepartmentDB.Expertise.Add(expertise);
                placementDepartmentDB.SaveChanges();
                return expertise.Id;
            }
        }
        public static void editExpertise(ExpertiseDto expertiseDto)
        {
            Expertise expertise = AutoMapperConfiguration.mapper.Map<Expertise>(expertiseDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Expertise.Attach(expertise);
                placementDepartmentDB.Entry(expertise).State = EntityState.Modified;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteExpertise(int id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                try
                {
                    Expertise expertise = placementDepartmentDB.Expertise.Find(id);
                    placementDepartmentDB.Expertise.Remove(expertise);
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
