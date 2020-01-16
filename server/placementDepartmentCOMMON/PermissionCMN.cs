using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class PermissionCMN
    {
        public PermissionCMN()
        {
            this.Users = new HashSet<UserCMN>();
        }

        public int Id { get; set; }
        public string description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCMN> Users { get; set; }
    }
}
