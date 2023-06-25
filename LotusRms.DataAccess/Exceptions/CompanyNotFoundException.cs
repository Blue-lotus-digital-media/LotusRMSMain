using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Exceptions
{
    public class CompanyNotFoundException:Exception
    {
        public CompanyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CompanyNotFoundException() : base("Company not found.")
        {
        }

        public CompanyNotFoundException(string message) : base(message)
        {
        }
    }
}
