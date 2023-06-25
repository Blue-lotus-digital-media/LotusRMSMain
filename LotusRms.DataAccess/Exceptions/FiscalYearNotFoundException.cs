using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Exceptions
{
    public class FiscalYearNotFoundException:Exception
    {
        public FiscalYearNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FiscalYearNotFoundException() : base("Fiscal year not found.")
        {
        }

        public FiscalYearNotFoundException(string message) : base(message)
        {
        }
    }
}
