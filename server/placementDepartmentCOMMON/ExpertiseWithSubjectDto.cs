using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class ExpertiseWithSubjectDto
    {
        public ExpertiseWithSubjectDto() { }

        public  ExpertiseDto expertise { get; set; }
        public  SubjectDto subject { get; set; }

    }
}
