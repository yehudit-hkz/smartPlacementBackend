using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using placementDepartmentCOMMON;

namespace placementDepartmentDAL
{
        public static class ContactManager
        {
            public static List<ContactDto> ContactListByCompany(int idCompany)
            {
                List<ContactDto> contactDtos;
                using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
                {
                    contactDtos = placementDepartmentDB.Contact
                        .Where(cn=>cn.companyId==idCompany)
                        .ProjectTo<ContactDto>(AutoMapperConfiguration.config)
                        .ToList();
                    return contactDtos;
                }
            }
            public static ContactDto ContactById(int Id)
            {
                Contact Ret;
                using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
                {
                    Ret = placementDepartmentDB.Contact.Find(Id);
                    return AutoMapperConfiguration.mapper.Map<ContactDto>(Ret);
                }
            }
            public static void NewContact(ContactDto contactDto)
            {
                Contact contact = AutoMapperConfiguration.mapper.Map<Contact>(contactDto);
                using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
                {
                placementDepartmentDB.Configuration.ValidateOnSaveEnabled = false;
                placementDepartmentDB.Contact.Add(contact);
                        placementDepartmentDB.SaveChanges();
                }
            }
            public static void ContactEditing(ContactDto contactDto)
            {
                Contact contact = AutoMapperConfiguration.mapper.Map<Contact>(contactDto);
                using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
                {
                    placementDepartmentDB.Contact.Attach(contact);
                    placementDepartmentDB.Entry(contact).State = EntityState.Modified;
                    placementDepartmentDB.SaveChanges();
                }
            }
            public static void DeleteContact(int id)
            {
                using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
                {
                    Contact Contact = placementDepartmentDB.Contact.Find(id);
                    placementDepartmentDB.Contact.Remove(Contact);
                    placementDepartmentDB.SaveChanges();
                }
            }
    }
}
