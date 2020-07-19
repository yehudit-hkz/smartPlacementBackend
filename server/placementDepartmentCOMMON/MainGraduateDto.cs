using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class MainGraduateDto
    {
        public string Id { get; set; }

        //public string lastName { get; set; }
        //public string firstName { get; set; }
        public string Name { get; set; }
        public string endYear { get; set; }

        // public bool didGraduate { get; set; }
        // public bool hasDiploma { get; set; }
        // public bool hasExperience { get; set; }
        public bool isActive { get; set; }
        public string branchName { get; set; }
        public string expertiseName { get; set; }

        //public virtual BranchDto Branch { get; set; }
        //public virtual ExpertiseDto Expertise { get; set; }
    }
}
