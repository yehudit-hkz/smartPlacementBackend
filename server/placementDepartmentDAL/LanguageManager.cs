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
    public static class LanguageManager
    {
        public static List<LanguageDto> LanguageList()
        {
            List<LanguageDto> LanguageDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                LanguageDtos = placementDepartmentDB.Language
                   .ProjectTo<LanguageDto>(AutoMapperConfiguration.config)
                   .ToList();
                return LanguageDtos;
            }
        }
        public static int NewLanguage(LanguageDto LanguageDto)
        {
            Language Language = AutoMapperConfiguration.mapper.Map<Language>(LanguageDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Language = placementDepartmentDB.Language.Add(Language);
                placementDepartmentDB.SaveChanges();
                return Language.Id;
            }
        }
        public static void editLanguage(LanguageDto LanguageDto)
        {
            Language Language = AutoMapperConfiguration.mapper.Map<Language>(LanguageDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Language.Attach(Language);
                placementDepartmentDB.Entry(Language).State = EntityState.Modified;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteLanguage(int id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                try
                {
                    Language Language = placementDepartmentDB.Language.Find(id);
                    placementDepartmentDB.Language.Remove(Language);
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
