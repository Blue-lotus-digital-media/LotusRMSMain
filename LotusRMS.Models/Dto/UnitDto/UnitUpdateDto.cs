﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.UnitDto
{
    public class UnitUpdateDto : UnitCreateDto
    {
        public Guid Id { get; set; }
    }
}
