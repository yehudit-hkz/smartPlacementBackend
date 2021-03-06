﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class GraduateFilters
    {
        public string name { get; set; }
        public List<string> gender { get; set; }
        public List<bool> active { get; set; }
        public List<bool> didGraduate { get; set; }
        public List<bool> hasDiploma { get; set; }
        public List<bool> isWork { get; set; }
        public int period { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public List<int> expertise { get; set; }
        public List<int> branch { get; set; }
    }
}
