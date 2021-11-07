using System;
using System.Diagnostics;
using System.Linq;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces;
using Infrastructure.Shared;

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
        public Domain.Entities.Account Create(Domain.Entities.Account account)
        {
            var dbPerson = GetOrCreatePerson(account);
            var dbAccount = GetAccount.IfActiveOrDefault(account.Person.Id, _context);
            if (dbAccount != null) throw new ServerException(Error.AccountAlreadyExists);
   
            dbAccount = new Account { Person = dbPerson };
            
            try
            {
                _context.Accounts.Add(dbAccount);
                _context.SaveChanges();

                account.AccountNumber = dbAccount.Id;
                return account;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw new ServerException(Error.AccountCreateFail);
            }
        }

        public Domain.Entities.Account Get(int accountNumber)
        {
            if (accountNumber < 1) throw new ServerException(Error.AccountInvalidId);

            var account = GetAccount.IfActiveById(accountNumber, _context);
            var currentBalance = GetBalance.Current(account.TransactionLogs);
            return BuildInstance.AccountEntity(account, currentBalance);
        }

        public Domain.Entities.Account Get(string ownerDoc)
        {
            if (string.IsNullOrEmpty(ownerDoc)) throw new ServerException(Error.PersonInvalidDoc);

            var account = GetAccount.IfActiveByOwnerDoc(ownerDoc, _context);
            var currentBalance = GetBalance.Current(account.TransactionLogs);
            return BuildInstance.AccountEntity(account, currentBalance);
        }
        
        public bool Delete(Domain.Entities.Account acc)
        {
            if (acc.AccountNumber < 1) throw new ServerException(Error.AccountInvalidId);

            var dbAccount = GetAccount.IfActiveById(acc.AccountNumber, _context);
            dbAccount.IsActive = false;

            try
            {
                _context.Accounts.Update(dbAccount);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
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
                accountEntity.Person = BuildInstance.NaturalPerson(account.Person);
                return accountEntity;
            }
            else
            {
                accountEntity.Person = BuildInstance.LegalPerson(account.Person);
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
            var dbPerson = GetPerson.ByDocsIfActive(docs, _context);
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
        
        private Person GetOrCreatePerson(Domain.Entities.Account account)
        {
            int personType = GetPerson.Type(account.Person);
            if (personType == 0) throw new ServerException(Error.PersonInvalidType);

            var dbPerson = GetPerson.ByDocsOrDefault(account.Person.Doc, _context);
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

            ActivatePerson(dbPerson);

            return dbPerson;
        }

        private void ActivatePerson(Person dbPerson)
        {
            if (dbPerson.IsActive == false)
            {
                dbPerson.IsActive = true;
                dbPerson.UpdatedAt = DateTime.Now;
            }
        }
    }
}

