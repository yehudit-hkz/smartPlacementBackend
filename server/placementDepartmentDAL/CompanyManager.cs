﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            //    //.Skip(p-1*s).Take(s)
            //    return companyDtos;
            //}

            List<Company> companies;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                companies = placementDepartmentDB.Company.ToList();
                //.Skip(p-1*s).Take(s)
                companyDtos = AutoMapperConfiguration.mapper.Map<List<CompanyDto>>(companies);
                return companyDtos  ;
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
                Company company = placementDepartmentDB.Company.Find(id);
                placementDepartmentDB.Company.Remove(company);
                placementDepartmentDB.SaveChanges();
            }
        }
    }
}