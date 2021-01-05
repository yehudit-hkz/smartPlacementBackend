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
    public static class CompanyManager
    {
        public static List<CompanyDto> CompanyList()
        {
            List<CompanyDto> companyDtos;

            //why is it not work like other
            //using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            //{
            //    companyDtos = placementDepartmentDB.Company
            //        .ProjectTo<CompanyDto>(AutoMapperConfiguration.config)
            //        .ToList();
            //    return companyDtos;
            //}

            List<Company> companies;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                companies = placementDepartmentDB.Company.ToList();
                companyDtos = AutoMapperConfiguration.mapper.Map<List<CompanyDto>>(companies);
                return companyDtos;
            }
        }
        public static List<CompanyDto> CompanyListByFilters(CompanyFilters filters)
        {
            List<CompanyDto> companyDtos;
            List<Company> companies;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                companies = placementDepartmentDB.Company
                    .Where(cmp =>
                    (filters.mainSubject == 0 && filters.subjectByJobs == 0) ||
                    (filters.mainSubject != 0 && cmp.mainField == filters.mainSubject) ||
                    (filters.subjectByJobs != 0 &&
                        cmp.Contact.
                            Where(cnt => cnt.Job.
                            Where(j => j.subjectId == filters.subjectByJobs).ToList().Count > 0)
                        .ToList().Count > 0
                    ))
                    .ToList();
                companyDtos = AutoMapperConfiguration.mapper.Map<List<CompanyDto>>(companies);
                return companyDtos;
            }
        }

        public static CompanyDto CompanyById(int Id)
        {
            Company Ret;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                Ret = placementDepartmentDB.Company.Find(Id);
                return AutoMapperConfiguration.mapper.Map<CompanyDto>(Ret);
            }
        }
        public static void NewCompany(CompanyDto companyDto)
        {
            Company company = AutoMapperConfiguration.mapper.Map<Company>(companyDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Company.Add(company);
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void CompanyEditing(CompanyDto companyDto)
        {
            Company company = AutoMapperConfiguration.mapper.Map<Company>(companyDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Company.Attach(company);
                placementDepartmentDB.Entry(company).State = EntityState.Modified;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteCompany(int id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                try
                {
                    Company company = placementDepartmentDB.Company.Find(id);
                    placementDepartmentDB.Company.Remove(company);
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
