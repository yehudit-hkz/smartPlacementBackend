using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class JobCMN
    {
        public JobCMN()
        {
            this.Candidates = new HashSet<CoordinatingJobsForGraduatesCMN>();
        }

        public int Id { get; set; }
        public System.DateTime dateReceived { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool isActive { get; set; }
        public bool didSendCV { get; set; }
        public System.DateTime lastUpdateDate { get; set; }

        public virtual ContactCMN Contact { get; set; }
        public virtual ICollection<CoordinatingJobsForGraduatesCMN> Candidates { get; set; }
        public virtual UserCMN UserGetting { get; set; }
        public virtual UserCMN UserHandles { get; set; }
        public virtual ReasonForClosingThePositionCMN ReasonForClosing { get; set; }
        public virtual SubjectCMN subject { get; set; }
    }
}
