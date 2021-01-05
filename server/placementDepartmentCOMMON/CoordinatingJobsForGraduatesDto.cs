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
        public DateTime dateReceived { get; set; }
        public Nullable<DateTime> lastUpdateDate { get; set; }
        public string candidateId { get; set; }
        public string candidateName { get; set; }
        public string linkToCV { get; set; }
        public int jobId { get; set; }
        public SubjectDto jobSubject { get; set; }
        public string companyName { get; set; }


        public virtual JobCoordinationStatusDto Status { get; set; }
    }
}
