﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace placementDepartmentCOMMON
{
    public class UserDto
    {
        public UserDto() { }
        
        public int Id { get; set; }
        public string name { get; set; }      
        public string email { get; set; }
        public string password { get; set; }
        public virtual PermissionDto Permission { get; set; }

      
    }
}
