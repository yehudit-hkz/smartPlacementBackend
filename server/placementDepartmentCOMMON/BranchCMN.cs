using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class BranchCMN
    {
        public BranchCMN()
        {
            this.Graduate = new HashSet<GraduateCMN>();
        }

        public int Id { get; set; }
        public string name { get; set; }

        public virtual ICollection<GraduateCMN> Graduate { get; set; }
    }
}
