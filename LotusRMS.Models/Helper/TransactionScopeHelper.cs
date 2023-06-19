using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LotusRMS.Models.Helper
{
    internal static class TransactionScopeHelper
    {
        /// <summary>
        ///     This method should be used to get instance of transaction scope when using async methods.
        ///     Without the TransactionScopeAsyncFlowOption.Enabled, the exception wont flow.
        /// </summary>
        /// <returns></returns>
        public static TransactionScope GetInstance()
        {
            return new TransactionScope(TransactionScopeOption.RequiresNew,TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
