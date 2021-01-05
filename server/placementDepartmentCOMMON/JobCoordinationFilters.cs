using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class JobCoordinationFilters
    {
        public List<int> status { get; set; }
        public List<int> branch { get; set; }
        public List<string> gender { get; set; }
        public List<int> subject { get; set; }
        public List<int> user { get; set; }
        public int period { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int curUserId { get; set; }
    }
}
