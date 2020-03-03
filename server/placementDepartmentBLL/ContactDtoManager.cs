using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public static class ContactDtoManager
    {
        public static List<ContactDto> ContactDtoListByCompany(int idCompany)
        {
            return ContactManager.ContactListByCompany(idCompany);
        }
        public static ContactDto ContactDtoById(int Id)
        {
            return ContactManager.ContactById(Id);
        }
        public static void NewContactDto(ContactDto ContactDto)
        {
            ContactManager.NewContact(ContactDto);
        }
        public static void ContactDtoEditing(ContactDto ContactDto)
        {
            ContactManager.ContactEditing(ContactDto);
        }
        public static void DeleteContactDto(int id)
        {
            ContactManager.DeleteContact(id);
        }
    }
}
