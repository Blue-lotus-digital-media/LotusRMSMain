﻿using LotusRMS.Models.Dto.BillSettingDTO;
using LotusRMS.Models.Dto.InvoiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IInvoiceService
    {
        Guid Create(Guid Id);
        void PrintCopy(Guid Id);
        IEnumerable<LotusRMS_Invoice> GetAll();
        LotusRMS_Invoice GetByGuid(Guid Id);
        LotusRMS_Invoice GetFirstOrDefault(Guid Id);


    }
}
