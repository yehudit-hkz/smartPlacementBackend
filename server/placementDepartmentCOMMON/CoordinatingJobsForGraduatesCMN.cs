using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class CoordinatingJobsForGraduatesCMN
    {
        public int Id { get; set; }
        public System.DateTime dateReceived { get; set; }
        public Nullable<System.DateTime> lastUpdateDate { get; set; }

        public virtual GraduateCMN Candidate { get; set; }
        public virtual JobCMN Job { get; set; }
        public virtual JobCoordinationStatusCMN Status { get; set; }
    }
}
