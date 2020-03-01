using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public static class ExpertiseDtoManager
    {
            public static List<ExpertiseDto> ExpertiseDtoList()
            {
                return ExpertiseManager.ExpertiseList();
            }
            public static void newDtoExpertise(ExpertiseDto expertiseDto)
            {
                ExpertiseManager.NewExpertise(expertiseDto);
            }
            public static void DeleteDtoExpertise(int id)
            {
                ExpertiseManager.DeleteExpertise(id);
            }
        }
}
