using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class JobDto
    {
        public JobDto() { }

        public int Id { get; set; }
        public DateTime dateReceived { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool isActive { get; set; }
        public bool didSendCV { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public int contactId { get; set; }
        public string contactName { get; set; }
        public int companyId { get; set; }
        public string companyName { get; set; }
        public int gettingId { get; set; }
        public string gettingName { get; set; }
        public int handlesId { get; set; }
        public string handlesName { get; set; }

        public virtual ReasonForClosingThePositionDto ReasonForClosing { get; set; }
        public virtual SubjectDto Subject { get; set; }
    }
}
