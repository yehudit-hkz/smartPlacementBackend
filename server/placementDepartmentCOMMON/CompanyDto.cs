using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class CompanyDto
    {
        public CompanyDto() { }
       
        public int Id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string descriptiovOfActivity { get; set; }
        public virtual CityDto City { get; set; }

        public virtual SubjectDto Subject { get; set; }
    }
}
