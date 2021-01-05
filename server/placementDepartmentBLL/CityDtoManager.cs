using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public static class CityDtoManager
    {
        public static List<CityDto> CityDtoList()
        {
            return CityManager.CityList();
        }
        public static int newDtoCity(CityDto CityDto)
        {
            return CityManager.NewCity(CityDto);
        }
        public static void editDtoCity(CityDto CityDto)
        {
            CityManager.editCity(CityDto);
        }
        public static void DeleteDtoCity(int id)
        {
            CityManager.DeleteCity(id);
        }
    }
}
