﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Roles
{
    public class ManageUserRolesVM
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; } = false;
    }
}
