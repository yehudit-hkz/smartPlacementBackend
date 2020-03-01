using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using placementDepartmentCOMMON;

namespace placementDepartmentDAL
{
    public static class BranchManager
    {
        public static List<BranchDto> BranchList()
        {
            List<BranchDto> branchDtos;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                branchDtos = placementDepartmentDB.Branch
                   .ProjectTo<BranchDto>(AutoMapperConfiguration.config)
                   .ToList();
                return branchDtos;
            }
        }
        public static void NewBranch(BranchDto branchDto)
        {
            Branch branch = AutoMapperConfiguration.mapper.Map<Branch>(branchDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Branch.Add(branch);
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteBranch(int id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Branch branch = placementDepartmentDB.Branch.Find(id);
                placementDepartmentDB.Branch.Remove(branch);
                placementDepartmentDB.SaveChanges();
            }
        }
    }
}
