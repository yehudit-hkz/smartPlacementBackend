using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace placementDepartmentCOMMON
{
    public class UserDto
    {
        public UserDto() { }
        
        public int Id { get; set; }
        public string name { get; set; }      
        public string email { get; set; }

        [JsonIgnore‏]
        public string password { get; set; }
        public bool isInitialPassword { get; set; }
        public string token { get; set; }

        public bool isActive { get; set; }
        public virtual PermissionDto Permission { get; set; }
    }
}
