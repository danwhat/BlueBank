using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared
{
    static class GetAccount
    {
        internal static Account GetActiveAccountById(int accNumber, BlueBankContext context)
        {
            return context.Accounts
                .Where(account => account.Id == accNumber && account.IsActive == true)
                .FirstOrDefault<Account>();
        }
    }
}
