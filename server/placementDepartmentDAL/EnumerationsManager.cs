using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using placementDepartmentCOMMON;

namespace placementDepartmentDAL
{
   public static class EnumerationsManager
    {
        public static List<CityDto> CityList()
        {
            List<CityDto> cityDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                cityDtos = placementDepartmentDB.City
                    .ProjectTo<CityDto>(AutoMapperConfiguration.config)
                    .ToList();
                return cityDtos;
            }
        }
        public static List<LanguageDto> LanguageList()
        {
            List<LanguageDto> languageDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                languageDtos = placementDepartmentDB.Language
                    .ProjectTo<LanguageDto>(AutoMapperConfiguration.config)
                    .ToList();
                return languageDtos;
            }
        }
        public static List<ReasonForClosingThePositionDto> ReasonForClosingList()
        {
            List<ReasonForClosingThePositionDto> reasonForClosingThePositionDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                reasonForClosingThePositionDtos = placementDepartmentDB.ReasonForClosingThePosition
                    .ProjectTo<ReasonForClosingThePositionDto>(AutoMapperConfiguration.config)
                    .ToList();
                return reasonForClosingThePositionDtos;
            }
        }
        public static List<JobCoordinationStatusDto> JobCoordinationStatusList()
        {
            List<JobCoordinationStatusDto> jobCoordinationStatusDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                jobCoordinationStatusDtos = placementDepartmentDB.JobCoordinationStatus
                    .ProjectTo<JobCoordinationStatusDto>(AutoMapperConfiguration.config)
                    .ToList();
                return jobCoordinationStatusDtos;
            }
        }
    }
}
