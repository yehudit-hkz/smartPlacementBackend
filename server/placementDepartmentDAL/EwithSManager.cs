using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;

namespace placementDepartmentDAL
{
    public static class EwithSManager
    {
        public static List<ExpertiseWithSubjectDto> EwithSList()
        {
            List<ExpertiseWithSubjectDto> EwithSDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                EwithSDtos = placementDepartmentDB.Expertise
                   .SelectMany(e => e.Subject.Select(s =>
                   new ExpertiseWithSubjectDto()
                   {
                       expertise = new ExpertiseDto()
                       {
                           Id = e.Id,
                           name = e.name
                       },
                       subject = new SubjectDto()
                       {
                           Id = s.Id,
                           name = s.name
                       }
                   }
                   )).ToList();
                return EwithSDtos;
            }
        }
        public static void NewEwithS(int idExpertise, SubjectDto subjectDto)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Expertise expertise = placementDepartmentDB.Expertise.Find(idExpertise);
                Subject subject = placementDepartmentDB.Subject.Find(subjectDto.Id);
                if(!expertise.Subject.Contains(subject))
                {
                    expertise.Subject.Add(subject);
                    placementDepartmentDB.SaveChanges();
                }
                else
                {
                    throw new Exception("Duplicate");
                }
            }
        }
        public static void DeleteEwithS(int idExpertise, int idSubject)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Expertise expertise = placementDepartmentDB.Expertise.Find(idExpertise);
                Subject subject = expertise.Subject.Single(s => s.Id == idSubject);
                expertise.Subject.Remove(subject);
                placementDepartmentDB.SaveChanges();
            }
        }
    }
}
