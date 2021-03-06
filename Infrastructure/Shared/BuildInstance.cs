using System.Linq;
using Domain.Core.Exceptions;
using Domain.Entities;

namespace Infrastructure.Shared
{
    internal static class BuildInstance
    {
        internal static NaturalPerson NaturalPerson(Person dbPerson)
        {
            return new Domain.Entities.NaturalPerson
            {
                Cpf = dbPerson.Doc,
                Name = dbPerson.Name,
                Address = dbPerson.Address,
                PhoneNumbers = dbPerson.Contacts?
                    .Select(contact => contact.PhoneNumber).ToList(),
                CreatedAt = dbPerson.CreatedAt,
                UpdatedAt = dbPerson.UpdatedAt
            };
        }
        internal static LegalPerson LegalPerson(Person dbPerson)
        {
            return new Domain.Entities.LegalPerson
            {
                Cnpj = dbPerson.Doc,
                Name = dbPerson.Name,
                Address = dbPerson.Address,
                PhoneNumbers = dbPerson.Contacts
                    .Select(contact => contact.PhoneNumber).ToList(),
                CreatedAt = dbPerson.CreatedAt,
                UpdatedAt = dbPerson.UpdatedAt
            };
        }

        internal static Domain.Entities.Account AccountEntity(Account dbAccount, decimal currentBalance = 0)
        {
            var accountEntity = new Domain.Entities.Account
            {
                AccountNumber = dbAccount.Id,
                Balance = currentBalance,
                CreatedAt = dbAccount.CreatedAt,
                UpdatedAt = dbAccount.UpdatedAt,
            };

            accountEntity.Person = (dbAccount.Person.Type == 1)
                ? BuildInstance.NaturalPerson(dbAccount.Person)
                : BuildInstance.LegalPerson(dbAccount.Person);

            return accountEntity;
        }

        internal static Domain.Entities.Transaction TransactionEntity(Transaction dbTransaction)
        {
            return new Domain.Entities.Transaction
            {
                AccountFrom = AccountEntity(dbTransaction.AccountFrom, 0),
                AccountTo = AccountEntity(dbTransaction.AccountTo, 0),
                Value = dbTransaction.Value,
                CreatedAt = dbTransaction.CreatedAt,
            };
        }
    }
}
