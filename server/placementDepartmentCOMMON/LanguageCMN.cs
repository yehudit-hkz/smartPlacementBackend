using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class LanguageCMN
    {
        public LanguageCMN()
        {
            this.GraduateLanguages = new HashSet<GraduateLanguagesCMN>();
        }

        public int Id { get; set; }
        public string name { get; set; }


        public virtual ICollection<GraduateLanguagesCMN> GraduateLanguages { get; set; }

    }
}
