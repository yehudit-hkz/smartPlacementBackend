using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class JobCoordinationStatusCMN
    {
        public JobCoordinationStatusCMN()
        {
            this.CoordinatingJobsForGraduates = new HashSet<CoordinatingJobsForGraduatesCMN>();
        }

        public int Id { get; set; }
        public string description { get; set; }

        public virtual ICollection<CoordinatingJobsForGraduatesCMN> CoordinatingJobsForGraduates { get; set; }
    }
}
