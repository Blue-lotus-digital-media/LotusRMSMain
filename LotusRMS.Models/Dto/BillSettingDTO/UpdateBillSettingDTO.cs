﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.BillSettingDTO
{
    public class UpdateBillSettingDTO:CreateBillSettingDTO
    {
        public Guid Id { get; set; }
    }
}
