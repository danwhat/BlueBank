using System;
using Domain.Core.Exceptions;

namespace Infrastructure.Shared
{
    internal static class Validate
    {
        internal static bool IsNull(Object obj)
        {
            return obj == null;
        }

        internal static bool Account(Domain.Entities.Account account)
        {
            var validAccount = account?.AccountNumber > 0;
            return validAccount;
        }

        internal static void TransactionDate(DateTime initial, DateTime final)
        {
            DateTime now = DateTime.Now;

            if (initial > final) throw new ServerException(Error.InitialDateInvalid);
            if (initial.Year == 0001 || final.Year == 0001) throw new ServerException(Error.DateInvalid);
            if (initial > now || final > now) throw new ServerException(Error.DateInvalid);
        }
    }
}