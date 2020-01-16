using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class ExpertiseCMN
    {
        public ExpertiseCMN()
        {
            this.Graduates = new HashSet<GraduateCMN>();
            this.Subjects = new HashSet<SubjectCMN>();
        }

        public int Id { get; set; }
        public string name { get; set; }

        public virtual ICollection<GraduateCMN> Graduates { get; set; }
        public virtual ICollection<SubjectCMN> Subjects { get; set; }
    }
}
