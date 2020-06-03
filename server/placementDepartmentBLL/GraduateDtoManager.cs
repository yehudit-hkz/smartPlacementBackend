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
        public static List<FullGraduateDto> GraduateDtoList()
        {
            return GraduateManager.GraduateList();
        }
        public static FullGraduateDto GraduateDtoById(string Id)
        {
            return GraduateManager.GraduateById(Id);
        }
        public static List<FullGraduateDto> GraduateDtoBySubject(int idSubject)
        {
            return GraduateManager.GraduateBySubject(idSubject);
        }
        public static void NewGraduateDto(FullGraduateDto graduateDto)
        {
            GraduateManager.NewGraduate(graduateDto);
        }
        public static void GraduateDtoEditing(FullGraduateDto graduateDto)
        {
            GraduateManager.GraduateEditing(graduateDto);
        }
        public static void GraduateDtoEditingtrue(string id, bool isint)
        {
            GraduateManager.GraduateEditingtrue(id,isint);
        }
        public static void DeleteGraduateDto(string id)
        {
            GraduateManager.DeleteGraduate(id);
        }

    }
}
