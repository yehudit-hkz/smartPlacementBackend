using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class JobFilters
    {
        //public string title { get; set; }
        public List<bool> sendCV { get; set; }
        public List<bool> active { get; set; }
        public int period { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public List<int> subjects { get; set; }
    }
}
    