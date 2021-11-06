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
                .OrderByDescending(item => item.CreatedAt)
                .FirstOrDefault<TransactionLog>();

            return dbBalance?.BalanceAfter ?? 0m;
        }
    }
}
