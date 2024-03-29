﻿using LotusRMS.Models.Dto.DueBookDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.CustomerDTO
{
    public class CreateCustomerDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string? PanOrVat { get; set; }
        public CreateDueBookDTO DueBook { get; set; }
    }
}
