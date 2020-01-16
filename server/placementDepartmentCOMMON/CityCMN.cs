using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class CityCMN
    {
        public CityCMN()
        {
            this.Companies = new HashSet<CompanyCMN>();
            this.Graduates = new HashSet<GraduateCMN>();
        }

        public int Id { get; set; }
        public string name { get; set; }
        public string area { get; set; }

        public virtual ICollection<CompanyCMN> Companies { get; set; }
        public virtual ICollection<GraduateCMN> Graduates { get; set; }
    }
}
