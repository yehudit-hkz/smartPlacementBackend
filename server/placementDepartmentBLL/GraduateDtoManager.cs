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
        public static ApiRes<MainGraduateDto> GraduateDtoLazyList(GraduateFilters filters, string sort, int page, int size)
        {
            return GraduateManager.GraduateLazyList(filters, sort, page, size);
        }
        public static List<MainGraduateDto> GraduateDtoList()
        {
            return GraduateManager.GraduateList();
        }
        public static FullGraduateDto GraduateDtoById(string Id)
        {
            return GraduateManager.GraduateById(Id);
        }
        public static List<FullGraduateDto> GraduateDtoForJob(int idSubject, int idJob)
        {
            return GraduateManager.GraduateForJob(idSubject, idJob);
        }
        public static FullGraduateDto NewGraduateDto(FullGraduateDto graduateDto, int userId)
        {
            return GraduateManager.NewGraduate(graduateDto);
        }
        public static void GraduateDtoEditing(FullGraduateDto graduateDto)
        {
            GraduateManager.GraduateEditing(graduateDto);
        }
        public static void GraduateDtoEditingtrue(string id, bool isint)
        {
            GraduateManager.GraduateEditingtrue(id, isint);
        }
        public static void GraduateDtoUploudFile(string graduateId, string filePath)
        {
            GraduateManager.GraduateUploudFile(graduateId, filePath);
        }
        public static void DeleteGraduateDto(string id)
        {
            GraduateManager.DeleteGraduate(id);
        }

    }
}
