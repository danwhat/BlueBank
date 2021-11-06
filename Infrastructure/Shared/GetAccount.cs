using Domain.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace Infrastructure.Shared
{
    static class GetAccount
    {        
        internal static Account IfActiveById(int accNumber, BlueBankContext context)
        {
            var dbAccount = context.Accounts
                .Where(account => account.Id == accNumber && account.IsActive == true)
                .Include(account => account.Person)
                    .ThenInclude(person => person.Contacts)
                .Include(account => account.TransactionLogs)         
                .FirstOrDefault<Account>();

            if (Validate.IsNull(dbAccount)) throw new ServerException(Error.AccountNotFound);
            return dbAccount;
        }

        internal static Account IfActiveByOwnerId(int ownerId, BlueBankContext context)
        {                        
            var dbAccount = context.Accounts
                .Where(account => account.PersonId == ownerId && account.IsActive == true)
                .Include(account => account.Person)
                    .ThenInclude(person => person.Contacts)
                .Include(account => account.TransactionLogs)                   
                .FirstOrDefault<Account>();           

            if (Validate.IsNull(dbAccount)) throw new ServerException(Error.AccountNotFound);
            return dbAccount;
        }

        internal static Account IfActiveByOwnerDoc(string ownerDoc, BlueBankContext context)
        {
            if (string.IsNullOrWhiteSpace(ownerDoc)) throw new ServerException(Error.PersonInvalidDoc);
            var dbPerson = GetPerson.IfActive(ownerDoc, context);
            if (Validate.IsNull(dbPerson)) throw new ServerException(Error.PersonNotFound);

            try
            {
                var dbAccount = IfActiveByOwnerId(dbPerson.Id, context);
                return dbAccount;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw new ServerException(Error.AccountNotFound);
            }           
        }
    }
}
