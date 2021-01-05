using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public static class LanguageDtoManager
    {
        public static List<LanguageDto> LanguageDtoList()
        {
            return LanguageManager.LanguageList();
        }
        public static int newDtoLanguage(LanguageDto LanguageDto)
        {
            return LanguageManager.NewLanguage(LanguageDto);
        }
        public static void editDtoLanguage(LanguageDto LanguageDto)
        {
            LanguageManager.editLanguage(LanguageDto);
        }
        public static void DeleteDtoLanguage(int id)
        {
            LanguageManager.DeleteLanguage(id);
        }
    }
}
