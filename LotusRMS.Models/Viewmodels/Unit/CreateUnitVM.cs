﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Unit
{
    public class CreateUnitVM
    {
        public string Unit_Name { get; set; }
        public string Unit_Symbol { get; set; }
        public string Unit_Description { get; set; }
        public ICollection<CreateUnitDivisionVM> Unit_Division { get; set; }
    }
    public class CreateUnitDivisionVM
    {
        public string Title { get; set; }
        public double Value { get; set; }

    }
}
