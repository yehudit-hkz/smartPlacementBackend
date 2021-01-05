using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoMapper.QueryableExtensions;
using placementDepartmentCOMMON;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace placementDepartmentDAL
{
    public static class CityManager
    {
        public static List<CityDto> CityList()
        {
            List<CityDto> CityDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                CityDtos = placementDepartmentDB.City
                   .ProjectTo<CityDto>(AutoMapperConfiguration.config)
                   .ToList();
                return CityDtos;
            }
        }
        public static int NewCity(CityDto CityDto)
        {
            City City = AutoMapperConfiguration.mapper.Map<City>(CityDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                City = placementDepartmentDB.City.Add(City);
                placementDepartmentDB.SaveChanges();
                return City.Id;
            }
        }
        public static void editCity(CityDto CityDto)
        {
            City City = AutoMapperConfiguration.mapper.Map<City>(CityDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.City.Attach(City);
                placementDepartmentDB.Entry(City).State = EntityState.Modified;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteCity(int id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                try
                {
                    City City = placementDepartmentDB.City.Find(id);
                    placementDepartmentDB.City.Remove(City);
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

