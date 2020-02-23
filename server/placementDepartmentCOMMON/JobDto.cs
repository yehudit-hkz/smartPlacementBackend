﻿using System;
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
        public System.DateTime dateReceived { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool isActive { get; set; }
        public bool didSendCV { get; set; }
        public System.DateTime lastUpdateDate { get; set; }
        public int contactId { get; set; }
        public string contactName { get; set; }
        public int userGettingId { get; set; }
        public string userGettingName { get; set; }
        public int UserHandlesId { get; set; }
        public string UserHandlesName { get; set; }

        public virtual ReasonForClosingThePositionDto ReasonForClosing { get; set; }
        public virtual SubjectDto Subject { get; set; }
    }
}