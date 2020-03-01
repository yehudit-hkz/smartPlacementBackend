using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using AutoMapper.QueryableExtensions;

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
        public static void NewExpertise(ExpertiseDto expertiseDto)
        {
            Expertise expertise = AutoMapperConfiguration.mapper.Map<Expertise>(expertiseDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Expertise.Add(expertise);
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteExpertise(int id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Expertise expertise = placementDepartmentDB.Expertise.Find(id);
                placementDepartmentDB.Expertise.Remove(expertise);
                placementDepartmentDB.SaveChanges();
            }
        }
    }
}
