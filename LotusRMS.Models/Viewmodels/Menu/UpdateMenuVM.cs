﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Menu
{
    public class UpdateMenuVM:CreateMenuVM
    {
        public Guid Id { get; set; }
        public Byte[]? Image { get; set; }
    }
}
