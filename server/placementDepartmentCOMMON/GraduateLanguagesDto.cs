using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class GraduateLanguagesDto
    {
        public string graduateId { get; set; }
        public int languageId { get; set; }
        public Nullable<int> level { get; set; }
        public string languageName { get; set; }
    }
}
