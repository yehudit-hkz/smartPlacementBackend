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
                //List<CoordinatingJobsForGraduates> JobsCoordinations;
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
            public static List<CoordinatingJobsForGraduates> NewJobsCoordination(List<CoordinatingJobsForGraduates> JobsCoordination)
            {
            List<CoordinatingJobsForGraduates> res;
                using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
                {
               
                var status = placementDepartmentDB.JobCoordinationStatus.Where(s=>s.description=="נשלחה הצעה").FirstOrDefault();
                JobsCoordination.ForEach(jc => {
                    jc.JobCoordinationStatus = status;
                    jc.placementStatus = status.Id;
                });
                  res=placementDepartmentDB.CoordinatingJobsForGraduates.AddRange(JobsCoordination).ToList();
                    placementDepartmentDB.SaveChanges();
                    return res;
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
        public static void JobsCoordinationUpdate(int idCoordinatingJobsForGraduate, string statusString)
        {
            CoordinatingJobsForGraduates coordination = new CoordinatingJobsForGraduates()
            {
                Id = idCoordinatingJobsForGraduate,
                lastUpdateDate = DateTime.Now
            };
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                var status = placementDepartmentDB.JobCoordinationStatus.Where(s => s.description == statusString).FirstOrDefault();
                coordination.placementStatus = status.Id;
                placementDepartmentDB.Configuration.ValidateOnSaveEnabled = false;
                placementDepartmentDB.CoordinatingJobsForGraduates.Attach(coordination);
                placementDepartmentDB.Entry(coordination).Property(x => x.placementStatus).IsModified = true;
                placementDepartmentDB.Entry(coordination).Property(x => x.lastUpdateDate).IsModified = true;
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
