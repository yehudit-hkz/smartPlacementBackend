using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
        public class ChartsDetails
        {
            public int type { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            public List<int> branches { get; set; }
            public List<string> areas { get; set; }
    }
}
