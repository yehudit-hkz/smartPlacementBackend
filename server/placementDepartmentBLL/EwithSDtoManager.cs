using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public static class EwithSDtoManager
    {
        public static List<ExpertiseWithSubjectDto> SwithEDtoList()
        {
            return EwithSManager.EwithSList();
        }
        public static void newDtoSubject(int idExpertise, SubjectDto subjectDto)
        {
             EwithSManager.NewEwithS(idExpertise, subjectDto);
        }
        public static void DeleteDtoSubject(int idExpertise, int idSubject)
        {
            EwithSManager.DeleteEwithS(idExpertise, idSubject);
        }
    }
}
