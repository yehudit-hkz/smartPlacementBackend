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
        public static void newDtoSubject(SubjectDto subjectDto)
        {
            SubjectManager.NewSubject(subjectDto);
        }
        public static void DeleteDtoSubject(int id)
        {
            SubjectManager.DeleteSubject(id);
        }
    }
}
