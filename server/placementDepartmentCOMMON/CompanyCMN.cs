using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class CompanyCMN
    {
        public CompanyCMN()
        {
            this.Contacts = new HashSet<ContactCMN>();
        }

        public int Id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string descriptiovOfActivity { get; set; }

        public virtual CityCMN City { get; set; }
        public virtual SubjectCMN subject { get; set; }
        public virtual ICollection<ContactCMN> Contacts { get; set; }
    }
}
