using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class CoordinatingJobsForGraduatesDto
    {
        public int Id { get; set; }
        public System.DateTime dateReceived { get; set; }
        public Nullable<System.DateTime> lastUpdateDate { get; set; }
        public string candidateId { get; set; }
        public string candidateName { get; set; }
        public int jobId { get; set; }
        public SubjectDto jobSubject { get; set; }

        public virtual JobCoordinationStatusDto Status { get; set; }
    }
}
