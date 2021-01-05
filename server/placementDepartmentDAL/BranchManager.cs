using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
        public static int NewBranch(BranchDto branchDto)
        {
            Branch branch = AutoMapperConfiguration.mapper.Map<Branch>(branchDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                branch = placementDepartmentDB.Branch.Add(branch);
                placementDepartmentDB.SaveChanges();
                return branch.Id;
            }
        }
        public static void editBranch(BranchDto branchDto)
        {
            Branch branch = AutoMapperConfiguration.mapper.Map<Branch>(branchDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Branch.Attach(branch);
                placementDepartmentDB.Entry(branch).State = EntityState.Modified;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteBranch(int id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                try
                {
                    Branch branch = placementDepartmentDB.Branch.Find(id);
                    placementDepartmentDB.Branch.Remove(branch);
                    placementDepartmentDB.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException != null)
                    {
                        if (sqlException.Number == 547)
                        {
                            throw new DbUpdateException("547");
                        }
                    }
                }
            }
        }
    }
}
