using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
   public static class BranchDtoManager
    {
        public static List<BranchDto> BranchDtoList()
        {
            return BranchManager.BranchList();
        }
        public static void newDtoBranch(BranchDto branchDto)
        {
            BranchManager.NewBranch(branchDto);
        }
        public static void DeleteDtoBranch(int id )
        {
            BranchManager.DeleteBranch(id);
        }

    }
}
