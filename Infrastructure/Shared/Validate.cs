using Domain.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared
{
    static class Validate
    {
        static internal bool IsNull(Object obj)
        {
            return obj == null;
        }

        static internal bool Account(Domain.Entities.Account account)
        {
            var nullAccount = account?.AccountNumber < 1;
            return nullAccount;
        }

        static internal void TransactionDate(DateTime initial, DateTime final)
        {
            DateTime now = DateTime.Now;

            if (initial > final) throw new ServerException(Error.InitialDateInvalid);
            if (initial.Year == 0001 || final.Year == 0001) throw new ServerException(Error.DateInvalid);
            if (initial > now || final > now) throw new ServerException(Error.DateInvalid);            
        }
    }
}
