using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public static class JobsCoordinationDtoManager
    {
        public static List<CoordinatingJobsForGraduatesDto> JobsCoordinationDtoListByGraduate(string idGraduate)
        {
            return JobsCoordinationManager.JobsCoordinationListByGraduate(idGraduate);
        }
        public static List<CoordinatingJobsForGraduatesDto> JobsCoordinationDtoByJob(int IdJob)
        {
            return JobsCoordinationManager.JobsCoordinationListByJob(IdJob);
        }
        public static void NewJobsCoordinationDto(int idJob, List<FullGraduateDto> fullGraduateDtos)
        {
            List<CoordinatingJobsForGraduates> coordinatingJobs = new List<CoordinatingJobsForGraduates>();
            foreach (FullGraduateDto graduate in fullGraduateDtos)
            {
                coordinatingJobs.Add(
                    new CoordinatingJobsForGraduates()
                    {
                        candidateId = graduate.Id,
                        jobId = idJob,
                        dateReceived = DateTime.Now,
                        lastUpdateDate = DateTime.Now
                    });
            }
            coordinatingJobs= JobsCoordinationManager.NewJobsCoordination(coordinatingJobs);
            JobDto job= JobManager.JobById(idJob);
            for (int i = 0; i < coordinatingJobs.Count; i++)
            {
                if(fullGraduateDtos[i].Id==coordinatingJobs[i].candidateId)
                {
                    coordinatingJobs[i].Graduate =
                        new Graduate()
                        {
                            firstName = fullGraduateDtos[i].firstName,
                            linkToCV = fullGraduateDtos[i].linkToCV,
                            email = fullGraduateDtos[i].email
                        };
                }
                coordinatingJobs[i].Job = new Job()
                {
                    description = job.description,
                    title = job.title
                };
            }
            MailManager.sendjobOfferToGraduates(coordinatingJobs);
        }
        public static void sendingCandidateToContact(string massege, List<CoordinatingJobsForGraduatesDto> coordinatings)
        {
            List<FullGraduateDto> graduates = new List<FullGraduateDto>();
            foreach (var item in coordinatings)
            {
                graduates.Add(GraduateManager.GraduateById(item.candidateId));
            }
            List<string> detailes = JobManager.get0TitleAnd1ContactMailOfJobById(coordinatings[0].jobId);
            MailManager.sendCVCandidateToContact(detailes[0],massege, graduates,detailes[1]);
            foreach (var crd in coordinatings)
            {
                JobsCoordinationDtoUpdate(crd.Id, "נשלחו קו\"ח");
            }
            JobManager.JobUpdate(coordinatings[0].jobId, true);

        }
        public static void JobsCoordinationDtoEditing(CoordinatingJobsForGraduatesDto JobsCoordinationDto)
        {
            JobsCoordinationManager.JobsCoordinationEditing(JobsCoordinationDto);
        }

        public static void JobsCoordinationDtoUpdate( int idCRD,string status)
        {
            JobsCoordinationManager.JobsCoordinationUpdate(idCRD,status);
        }

        //public static void DeleteJobsCoordinationDto(int id)
        //{
        //    JobsCoordinationManager.DeleteJobsCoordination(id);
        //}
    }
}
