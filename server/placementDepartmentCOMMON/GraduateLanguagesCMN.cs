using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class GraduateLanguagesCMN
    {
        public string graduateId { get; set; }
        public int languageId { get; set; }
        public Nullable<int> level { get; set; }

        public virtual GraduateCMN Graduate { get; set; }
        public virtual LanguageCMN Language { get; set; }
    }
}
