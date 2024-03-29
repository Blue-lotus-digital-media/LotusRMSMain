﻿using System.Transactions;

namespace LotusRMSweb.Helper
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
            return new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}

