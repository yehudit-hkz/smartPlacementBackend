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
        public static class JobsCoordinationManager
        {
            public static List<CoordinatingJobsForGraduatesDto> JobsCoordinationListByGraduate(string idGraduate)
            {
                List<CoordinatingJobsForGraduatesDto> JobsCoordinationDtos;
                List<CoordinatingJobsForGraduates> JobsCoordinations;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
                {
                JobsCoordinationDtos = placementDepartmentDB.CoordinatingJobsForGraduates
                        .Where(cj => cj.candidateId== idGraduate)
                        .ProjectTo<CoordinatingJobsForGraduatesDto>(AutoMapperConfiguration.config)
                        .ToList();
               // JobsCoordinationDtos = AutoMapperConfiguration.mapper.Map<List<CoordinatingJobsForGraduatesDto>>(JobsCoordinations);

                return JobsCoordinationDtos;
                }
            }
            public static List<CoordinatingJobsForGraduatesDto> JobsCoordinationListByJob(int IdJob)
            {
               List<CoordinatingJobsForGraduatesDto> JobsCoordinationDtos;
                using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
                {
                JobsCoordinationDtos = placementDepartmentDB.CoordinatingJobsForGraduates
                    .Where(cj => cj.jobId == IdJob)
                    .ProjectTo<CoordinatingJobsForGraduatesDto>(AutoMapperConfiguration.config)
                    .ToList();
                return JobsCoordinationDtos;
                }
            }
            public static void NewJobsCoordination(CoordinatingJobsForGraduatesDto JobsCoordinationDto)
            {
            CoordinatingJobsForGraduates JobsCoordination = AutoMapperConfiguration.mapper.Map<CoordinatingJobsForGraduates>(JobsCoordinationDto);
                using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
                {
                    placementDepartmentDB.CoordinatingJobsForGraduates.Add(JobsCoordination);
                    placementDepartmentDB.SaveChanges();
                }
            }
            public static void JobsCoordinationEditing(CoordinatingJobsForGraduatesDto JobsCoordinationDto)
            {
            CoordinatingJobsForGraduates JobsCoordination = AutoMapperConfiguration.mapper.Map<CoordinatingJobsForGraduates>(JobsCoordinationDto);
                using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
                {
                    placementDepartmentDB.CoordinatingJobsForGraduates.Attach(JobsCoordination);
                    placementDepartmentDB.Entry(JobsCoordination).State = EntityState.Modified;
                    placementDepartmentDB.SaveChanges();
                }
            }
            public static void DeleteJobsCoordination(int id)
            {
                using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
                {
                CoordinatingJobsForGraduates JobsCoordination = placementDepartmentDB.CoordinatingJobsForGraduates.Find(id);
                    placementDepartmentDB.CoordinatingJobsForGraduates.Remove(JobsCoordination);
                    placementDepartmentDB.SaveChanges();
                }
            }
        }
    }
