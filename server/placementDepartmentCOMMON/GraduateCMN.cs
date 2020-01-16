﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace placementDepartmentCOMMON
{
    public class GraduateCMN
    {
        public GraduateCMN()
        {
            this.Jobs = new HashSet<CoordinatingJobsForGraduatesCMN>();
            this.Languages = new HashSet<GraduateLanguagesCMN>();
        }
    
        public string Id { get; set; }
        public string gender { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public System.DateTime dateOfBirth { get; set; }
        public string address { get; set; }
        public string zipCode { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string startYear { get; set; }
        public string endYear { get; set; }
        public System.DateTime dateOfRegistration { get; set; }
        public System.DateTime lastUpdate { get; set; }
        public bool didGraduate { get; set; }
        public bool hasDiploma { get; set; }
        public bool isWorkerInProfession { get; set; }
        public string companyName { get; set; }
        public string role { get; set; }
        public Nullable<bool> placedByThePlacementDepartment { get; set; }
        public bool hasExperience { get; set; }
        public bool isActive { get; set; }
        public string linkToCV { get; set; }
    
        public virtual BranchCMN Branch { get; set; }
        public virtual CityCMN City { get; set; }
        public virtual ICollection<CoordinatingJobsForGraduatesCMN> Jobs { get; set; }
        public virtual ExpertiseCMN Expertise { get; set; }
        public virtual ICollection<GraduateLanguagesCMN> Languages { get; set; }
    }
}
