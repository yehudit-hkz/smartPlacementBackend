using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class ReminderCMN
    {
        public int Id { get; set; }
        public string description { get; set; }
        public System.DateTime dateTime { get; set; }

        public virtual UserCMN User { get; set; }
    }
}
