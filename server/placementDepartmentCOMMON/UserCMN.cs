using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class UserCMN
    {
        public UserCMN()
        {
            this.JobsHeReceived = new HashSet<JobCMN>();
            this.JobsHandled = new HashSet<JobCMN>();
            this.Reminders = new HashSet<ReminderCMN>();
        }

        public int Id { get; set; }
        public string name { get; set; }      
        public string email { get; set; }
        public string password { get; set; }

        public virtual ICollection<JobCMN> JobsHeReceived { get; set; }
        public virtual ICollection<JobCMN> JobsHandled{ get; set; }
        public virtual PermissionCMN Permission { get; set; }
        public virtual ICollection<ReminderCMN> Reminders { get; set; }
    }
}
