using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class ChartData
    {
        public ChartData(string label)
        {
            this.label = label;
        }

        public string label { get; set; }
        public List<int> data { get; set; }
    }
}
