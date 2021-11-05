using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Shared
{
    static class GetAccount
    {
        internal static Account IfActiveById(int accNumber, BlueBankContext context)
        {
            return context.Accounts
                .Where(account => account.Id == accNumber && account.IsActive == true)
                .Include(account => account.Person)
                    .ThenInclude(person => person.Contacts)
                .Include(account => account.TransactionLogs)
                .FirstOrDefault<Account>();
        }

        internal static Account IfActiveByOwnerId(int ownerId, BlueBankContext context)
        {
            return context.Accounts
                .Where(account => account.PersonId == ownerId && account.IsActive == true)
                .Include(account => account.Person)
                    .ThenInclude(person => person.Contacts)
                .Include(account => account.TransactionLogs)
                .FirstOrDefault<Account>();
        }

        internal static Account IfActiveByOwnerDoc(string ownerDoc, BlueBankContext context)
        {
            var dbPerson = GetPerson.ByDocs(ownerDoc, context);

            return context.Accounts
                .Where(account => account.PersonId == dbPerson.Id && account.IsActive == true)
                .Include(account => account.Person)
                .Include(account => account.TransactionLogs)
                .FirstOrDefault<Account>();
        }
    }
}
