using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class ContactDto
    {
        public ContactDto() { }

        public int Id { get; set; }
        public string name { get; set; }
        public string officePhone { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

        public int companyId { get; set; }
        public string companyName { get; set; }

    }
}
