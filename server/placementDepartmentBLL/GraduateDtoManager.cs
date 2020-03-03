using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public static class GraduateDtoManager
    {
        public static List<GraduateDto> GraduateDtoList()
        {
            return GraduateManager.GraduateList();
        }
        public static GraduateDto GraduateDtoById(string Id)
        {
            return GraduateManager.GraduateById(Id);
        }
        public static List<GraduateDto> GraduateDtoBySubject(SubjectDto subjectDto)
        {
            return GraduateManager.GraduateBySubject(subjectDto);
        }
        public static void NewGraduateDto(GraduateDto graduateDto)
        {
            GraduateManager.NewGraduate(graduateDto);
        }
        public static void GraduateDtoEditing(GraduateDto graduateDto)
        {
            GraduateManager.GraduateEditing(graduateDto);
        }
        public static void DeleteGraduateDto(string id)
        {
            GraduateManager.DeleteGraduate(id);
        }
    }
}
