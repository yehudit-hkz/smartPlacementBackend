﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public static class ChartBLManager
    {
        public static List<ChartData> GetChart(ChartsDetails chartsDetails)
        {
            switch (chartsDetails.type)
            {
                case 1: return ChartManager.GetJobsOpenedChart(chartsDetails.start, chartsDetails.end, chartsDetails.userid, chartsDetails.curUserid);
                case 2: return ChartManager.GetGraduatesVSJobs(chartsDetails.branches,chartsDetails.areas, chartsDetails.userid, chartsDetails.curUserid);
                case 3: return ChartManager.GetPlacementsChart(chartsDetails.start, chartsDetails.end, chartsDetails.userid, chartsDetails.curUserid);
                default:
                    return null;
            }
            
        }
    }
}
