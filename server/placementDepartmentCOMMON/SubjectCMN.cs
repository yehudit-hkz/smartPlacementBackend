using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class SubjectCMN
    {
        public SubjectCMN()
        {
            this.Companies = new HashSet<CompanyCMN>();
            this.Jobs = new HashSet<JobCMN>();
            this.Expertise = new HashSet<ExpertiseCMN>();
        }

        public int Id { get; set; }
        public string name { get; set; }

        public virtual ICollection<CompanyCMN> Companies { get; set; }
        public virtual ICollection<JobCMN> Jobs { get; set; }
        public virtual ICollection<ExpertiseCMN> Expertise { get; set; }
    }
}
