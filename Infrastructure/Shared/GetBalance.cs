using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared
{
    static class GetBalance
    {
        internal static decimal Current(ICollection<TransactionLog> transactionLogs)
        {
            var dbBalance = transactionLogs?
                .OrderBy(item => item.CreatedAt)
                .First()
                .BalanceAfter;

            return dbBalance ?? 0m;
        }
    }
}
