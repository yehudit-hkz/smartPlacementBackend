//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace placementDepartmentDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CoordinatingJobsForGraduates
    {
        public int Id { get; set; }
        public System.DateTime dateReceived { get; set; }
        public int jobId { get; set; }
        public string candidateId { get; set; }
        public int placementStatus { get; set; }
        public Nullable<System.DateTime> lastUpdateDate { get; set; }
    
        public virtual Graduate Graduate { get; set; }
        public virtual Job Job { get; set; }
        public virtual JobCoordinationStatus JobCoordinationStatus { get; set; }
    }
}
