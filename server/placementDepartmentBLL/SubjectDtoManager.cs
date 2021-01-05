using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public static class SubjectDtoManager
    {
        public static List<SubjectDto> SubjectDtoList()
        {
            return SubjectManager.SubjectList();
        }
        public static int newDtoSubject(SubjectDto subjectDto)
        {
            return SubjectManager.NewSubject(subjectDto);
        }
        public static void editDtoSubject(SubjectDto subjectDto)
        {
            SubjectManager.editSubject(subjectDto);
        }
        public static void DeleteDtoSubject(int id)
        {
            SubjectManager.DeleteSubject(id);
        }
    }
}
