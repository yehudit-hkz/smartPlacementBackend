﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class ApiRes<T>
    {
        public List<T> items { get; set; }
        public int totalCount { get; set; }
    }
}
