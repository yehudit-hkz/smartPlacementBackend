using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class ContactCMN
    {
        public ContactCMN()
        {
            this.Jobs = new HashSet<JobCMN>();
        }

        public int Id { get; set; }
        public string name { get; set; }
        public string officePhone { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

        public virtual CompanyCMN Company { get; set; }
        public virtual ICollection<JobCMN> Jobs { get; set; }
    }
}
