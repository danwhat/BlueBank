using System;
using System.Linq;
using Domain.Core.Interfaces;
using Domain.Entities;
using Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BlueBankContext _context;

        public AccountRepository(BlueBankContext context)
        {
            _context = context;
        }

        #region Interface methods

        public Domain.Entities.Account Get(int accountNumber)
        {
            if (accountNumber < 1) return null;

            var account = GetAccount.IfActiveById(accountNumber, _context);
            if (account == null) return null;

            var currentBalance = GetBalance.Current(account.TransactionLogs);

            var accountEntity = new Domain.Entities.Account
            {
                AccountNumber = account.Id,
                Balance = currentBalance
            };

            if (account.Person.Type == 1)
            {
                accountEntity.Person = BuildInstance.NaturalPerson(account.Person);
                return accountEntity;
            }
            else
            {
                accountEntity.Person = BuildInstance.LegalPerson(account.Person);
                return accountEntity;
            }
        }

        public Domain.Entities.Account Get(string ownerDoc)
        {
            Account account = null;
            try
            {
                account = GetAccount.IfActiveByOwnerDoc(ownerDoc, _context);
                if (account == null) return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            var currentBalance = GetBalance.Current(account.TransactionLogs);

            var accountEntity = new Domain.Entities.Account
            {
                AccountNumber = account.Id,
                Balance = currentBalance
            };

            if (account.Person.Type == 1)
            {
                var personEntity = new Domain.Entities.NaturalPerson
                { Cpf = account.Person.Doc, Name = account.Person.Name, Address = account.Person.Address };
                accountEntity.Person = personEntity;
                return accountEntity;
            }
            else
            {
                var personEntity = new Domain.Entities.LegalPerson
                { Cnpj = account.Person.Doc, Name = account.Person.Name, Address = account.Person.Address };
                accountEntity.Person = personEntity;
                return accountEntity;
            }

        }
        
        public bool Delete(Domain.Entities.Account acc)
        {
            var dbAccount = GetAccount.IfActiveById(acc.AccountNumber, _context);
            if (dbAccount == null) return true;

            dbAccount.IsActive = false;

            try
            {
                _context.Accounts.Update(dbAccount);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Domain.Entities.Account Create(Domain.Entities.Account account)
        {
            int personType = 0;
            if (account.Person.GetType() == typeof(NaturalPerson)) personType = 1;
            if (account.Person.GetType() == typeof(LegalPerson)) personType = 2;
            if (personType == 0) return null;

            var dbPerson = GetPerson.ByDocs(account.Person.Doc, _context);
            if (dbPerson == null)
            {
                dbPerson = new Person
                {
                    Doc = account.Person.Doc,
                    Name = account.Person.Name,
                    Address = account.Person.Address,
                    Type = personType,
                };
            }

            if (dbPerson.IsActive == false) dbPerson.IsActive = true;
            dbPerson.UpdatedAt = DateTime.Now;

            var dbAccount = GetAccount.IfActiveByOwnerId(account.Person.Id, _context);
            if (dbAccount != null)
            {
                Console.WriteLine("Cliente já tem conta");
                return null;
            }

            dbAccount = new Account
            {
                Person = dbPerson,
            };

            try
            {
                _context.Accounts.Add(dbAccount);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                Console.WriteLine("Cliente já tem conta!");
            }

            account.AccountNumber = dbAccount.Id;
            return account;
        }

        #endregion        

        #region Extra methods

        private Domain.Entities.Account GetByPersonDoc(string docs)
        {
            // validacoes
            Account account = null;
            try
            {
                account = GetAccount.IfActiveByOwnerDoc(docs, _context);
                if (account == null) return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            var currentBalance = GetBalance.Current(account.TransactionLogs);

            var accountEntity = new Domain.Entities.Account
            {
                AccountNumber = account.Id,
                Balance = currentBalance
            };

            if (account.Person.Type == 1)
            {
                var personEntity = new Domain.Entities.NaturalPerson
                { Cpf = account.Person.Doc, Name = account.Person.Name, Address = account.Person.Address };
                accountEntity.Person = personEntity;
                return accountEntity;
            }
            else
            {
                var personEntity = new Domain.Entities.LegalPerson
                { Cnpj = account.Person.Doc, Name = account.Person.Name, Address = account.Person.Address };
                accountEntity.Person = personEntity;
                return accountEntity;
            }

        }

        private void Remove(Domain.Entities.Account account)
        {
            Account dbAccount = null;
            try
            {
                dbAccount = GetAccount.IfActiveByOwnerDoc(account.Person.Doc, _context);
                if (dbAccount == null) return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            dbAccount.IsActive = false;

            try
            {
                _context.Accounts.Update(dbAccount);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Remove(string docs)
        {
            var dbPerson = GetPerson.IfActive(docs, _context);
            if (dbPerson == null) throw new Exception();

            var dbAccount = _context.Accounts
                .Where(curr => curr.PersonId == dbPerson.Id && curr.IsActive == true)
                .FirstOrDefault<Account>();
            if (dbAccount == null) throw new Exception();

            dbAccount.IsActive = false;

            try
            {
                _context.Accounts.Update(dbAccount);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion
    }
}
