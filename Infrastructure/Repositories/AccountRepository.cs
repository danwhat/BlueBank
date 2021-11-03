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
            var account = GetActiveAccountById(accountNumber);
            if (account == null) return null;

            var currentBalance = account.Balances?.OrderByDescending(item => item.CreatedAt).First();

            var accountEntity = new Domain.Entities.Account
            {
                AccountNumber = account.Id,
                Balance = (currentBalance == null) ? 0 : currentBalance.Value
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

        public Domain.Entities.Account Get(string ownerDoc)
        {
            Account account = null;
            try
            {
                account = GetActiveAccountByOwnerDoc(ownerDoc);
                if (account == null) return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            var currentBalance = account.Balances?.OrderByDescending(item => item.CreatedAt).First();

            var accountEntity = new Domain.Entities.Account
            {
                AccountNumber = account.Id,
                Balance = (currentBalance == null) ? 0 : currentBalance.Value
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
            var dbAccount = GetActiveAccountById(acc.Id);
            if (dbAccount == null) throw new Exception();

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

        #endregion

        private Account GetActiveAccountById(int accNumber)
        {
            return _context.Accounts
                .Where(account => account.Id == accNumber && account.IsActive == true)
                .FirstOrDefault<Account>();
        }
        private Account GetActiveAccountByOwnerDoc(string docs)
        {
            var dbPerson = GetPerson.ByDocs(docs, _context);
            if (dbPerson == null) throw new Exception();
            return _context.Accounts
                .Where(account => account.PersonId == dbPerson.Id && account.IsActive == true)
                .FirstOrDefault<Account>();
        }

        #region Extra methods
        public Domain.Entities.Account GetByPersonDoc(string docs)
        {
            // validacoes
            Account account = null;
            try
            {
                account = GetActiveAccountByOwnerDoc(docs);
                if (account == null) return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            var currentBalance = account.Balances?.OrderByDescending(item => item.CreatedAt).First();

            var accountEntity = new Domain.Entities.Account
            {
                AccountNumber = account.Id,
                Balance = (currentBalance == null) ? 0 : currentBalance.Value
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

        public Domain.Entities.Account Create(Domain.Entities.Account account)
        {
            int personType = 0;
            if (account.Person.GetType() == typeof(NaturalPerson)) personType = 1;
            if (account.Person.GetType() == typeof(LegalPerson)) personType = 2;
            if (personType == 0) return null;

            var dbPerson = GetPerson.IsActive(account.Person.Doc, _context);
            if (dbPerson == null)
            {
                dbPerson = new Person
                {
                    Doc = account.Person.Doc,
                    Name = account.Person.Name,
                    Address = account.Person.Address,
                    Type = personType,
                    CreatedAt = DateTime.Now,
                };
            }

            if (dbPerson.IsActive == false) dbPerson.IsActive = true;
            dbPerson.UpdatedAt = DateTime.Now;

            var dbAccount = new Account()
            {
                Person = dbPerson,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
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

        public void Remove(Domain.Entities.Account account)
        {
            Account dbAccount = null;
            try
            {
                dbAccount = GetActiveAccountByOwnerDoc(account.Person.Doc); ;
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

        public void Remove(string docs)
        {
            var dbPerson = GetPerson.IsActive(docs, _context);
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
