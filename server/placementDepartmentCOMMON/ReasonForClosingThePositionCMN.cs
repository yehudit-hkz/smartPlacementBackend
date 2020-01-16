using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class ReasonForClosingThePositionCMN
    {
        public ReasonForClosingThePositionCMN()
        {
            this.Jobs = new HashSet<JobCMN>();
        }

        public int Id { get; set; }
        public string description { get; set; }

        public virtual ICollection<JobCMN> Jobs { get; set; }
    }
}
