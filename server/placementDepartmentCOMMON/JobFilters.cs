using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class JobFilters
    {
        public List<bool> sendCV { get; set; }
        public List<bool> active { get; set; }
        public List<int> ReasonClosing { get; set; }
        public int period { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public List<int> subjects { get; set; }
        public List<int> user { get; set; }
        public int curUserId { get; set; }
    }
}
    