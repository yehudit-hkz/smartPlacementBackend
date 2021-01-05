using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace placementDepartmentCOMMON
{
    public class FullGraduateDto
    {
        public FullGraduateDto()
        {
            Languages = new List<GraduateLanguagesDto>();
        }

        public string Id { get; set; }
        public string gender { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string address { get; set; }
        public string zipCode { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string startYear { get; set; }
        public string endYear { get; set; }
        public DateTime dateOfRegistration { get; set; }
        public DateTime lastUpdate { get; set; }
        public bool didGraduate { get; set; }
        public bool hasDiploma { get; set; }
        public bool isWorkerInProfession { get; set; }
        public string companyName { get; set; }
        public string role { get; set; }
        public Nullable<bool> placedByThePlacementDepartment { get; set; }
        public bool hasExperience { get; set; }
        public bool isActive { get; set; }
        public string linkToCV { get; set; }

        public virtual CityDto City { get; set; }
        public virtual BranchDto Branch { get; set; }
        public virtual ExpertiseDto Expertise { get; set; }

        public virtual List<GraduateLanguagesDto> Languages { get; set; }
    }
}
