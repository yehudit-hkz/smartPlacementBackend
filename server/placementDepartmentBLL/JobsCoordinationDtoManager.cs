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
        public static ApiRes<CoordinatingJobsForGraduatesDto> JobCoordinationDtoLazyList(JobCoordinationFilters filters, string sort, int page, int size)
        {
            return JobsCoordinationManager.JobCoordinationLazyList(filters, sort, page, size);
        }
        public static List<CoordinatingJobsForGraduatesDto> JobsCoordinationDtoListByGraduate(string idGraduate, int idUser)
        {
            return JobsCoordinationManager.JobsCoordinationListByGraduate(idGraduate,idUser);
        }
        public static List<CoordinatingJobsForGraduatesDto> JobsCoordinationDtoByJob(int IdJob)
        {
            return JobsCoordinationManager.JobsCoordinationListByJob(IdJob);
        }

        public static List<string> NewJobsCoordinationDto(int idJob, List<FullGraduateDto> fullGraduateDtos, int userId, string byEmail, string password,string url)
        {
            JobDto job = JobManager.JobById(idJob);
            List<CoordinatingJobsForGraduates> coordinatingJobs = new List<CoordinatingJobsForGraduates>();
            foreach (FullGraduateDto graduate in fullGraduateDtos)
            {
                coordinatingJobs.Add(
                    new CoordinatingJobsForGraduates()
                    {
                        candidateId = graduate.Id,
                        jobId = idJob,
                        placementStatus = 1,
                        dateReceived = DateTime.Now,
                        lastUpdateDate = DateTime.Now
                    });
            }
            coordinatingJobs= JobsCoordinationManager.NewJobsCoordination(coordinatingJobs);

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

            MailManager mail = new MailManager(byEmail, password, url);
            var errList = mail.sendjobOfferToGraduates(coordinatingJobs,userId);

            //delete JobsCoordination feild sending offer and return
            JobsCoordinationManager.DeleteJobsCoordination(errList.Select(crd => crd.Id).ToList());
            return errList.Select(crd=>$"{crd.Graduate.firstName} {crd.Graduate.lastName}").ToList();
        }
       
        public static void JobsCoordinationDtoEditing(CoordinatingJobsForGraduatesDto JobsCoordinationDto)
        {
            JobsCoordinationManager.JobsCoordinationUpdate(
                new List<int>() { JobsCoordinationDto.Id }, JobsCoordinationDto.Status.Id
                );
        }
        public static void JobsCoordinationDtoUpdate( int idCRD,int status)
        {
            JobsCoordinationManager.JobsCoordinationUpdate(new List<int>() { idCRD },status);
        }

        public static List<FullGraduateDto> sendingCandidateToContact(string massege, List<CoordinatingJobsForGraduatesDto> coordinatings, int userId, string byEmail, string password, string fldCVPath)
        {
            List<FullGraduateDto> graduates = GraduateManager.GraduateListById(
                coordinatings.Select(c => c.candidateId).ToList());

            List<string> detailes = JobManager.get0TitleAnd1ContactMailOfJobById(coordinatings[0].jobId);

            MailManager mail = new MailManager(byEmail,password,fldCVPath);
            graduates = mail.sendCVCandidateToContact(detailes[0], massege, graduates, detailes[1], userId);

            coordinatings.RemoveAll(c => graduates.Any(g => g.Id == c.candidateId));

            JobsCoordinationManager.JobsCoordinationUpdate(
                coordinatings.Select(c => c.Id).ToList()
                , 3); //send CV
            JobManager.JobUpdate(coordinatings[0].jobId, true);

            return graduates;

        }
    }
}
