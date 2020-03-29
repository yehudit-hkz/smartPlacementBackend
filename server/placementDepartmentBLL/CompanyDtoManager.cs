using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public static class CompanyDtoManager
    {
        public static List<CompanyDto> CompanyDtoList()
        {
            return placementDepartmentDAL.CompanyManager.CompanyList();
        }
        public static CompanyDto CompanyDtoById(int Id)
        {
            return CompanyManager.CompanyById(Id);
        }
        public static void NewCompanyDto(CompanyDto companyDto)
        {
            CompanyManager.NewCompany(companyDto);
        }
        public static void CompanyDtoEditing(CompanyDto companyDto)
        {
            CompanyManager.CompanyEditing(companyDto);
        }
        public static void DeleteCompanyDto(int id)
        {
            CompanyManager.DeleteCompany(id);
        }
    }
}
