using System;
using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AccountRepository
    {
        private readonly BlueBankContext _context;

        public AccountRepository(BlueBankContext context)
        {
            _context = context;
        }

        public Domain.Entities.Account GetByPersonDoc(string docs)
        {
            // validacoes
            var person = _context.People
                .Where(person => person.Doc == docs && person.isActive == true)
                .FirstOrDefault<Person>();
            if (person == null) throw new Exception();

            var account = _context.Accounts
                .Where(account => account.PersonId == person.Id && account.IsActive == true)
                .FirstOrDefault<Account>();
            if (account == null) return null;

            var currentBalance = account.Balances?.OrderByDescending(item => item.CreatedAt).First();

            var accountEntity = new Domain.Entities.Account 
            { 
                AccountNumber = account.Id,
                Balance = (currentBalance == null) ? 0 : currentBalance.Value
            };

            if (person.Type == 1)
            {
                var personEntity = new Domain.Entities.NaturalPerson
                    { Cpf = person.Doc, Name = person.Name, Address = person.Address };
                accountEntity.Person = personEntity;
                return accountEntity;
            }
            else
            {
                var personEntity = new Domain.Entities.LegalPerson
                    { Cnpj = person.Doc, Name = person.Name, Address = person.Address };
                accountEntity.Person = personEntity;
                return accountEntity;
            }

        }
        //public Domain.Entities.Account GetByPersonId()
        //public Domain.Entities.Account GetByPersonName()
        
        public Domain.Entities.Account Create(Domain.Entities.Account account)
        {
            int personType = 0;
            if (account.Person.GetType() == typeof(NaturalPerson)) personType = 1;
            if (account.Person.GetType() == typeof(LegalPerson)) personType = 2;
            if (personType == 0) return null;

            var dbPerson = _context.People
                .Where(person => person.Doc == account.Person.Doc && person.isActive == true)
                .FirstOrDefault<Person>();
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
            
            if (dbPerson.isActive == false) dbPerson.isActive = true;
            dbPerson.UpdatedAt = DateTime.Now;
            
            var dbAccount = new Account() { 
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
            var dbPerson = _context.People
                .Where(person => person.Doc == account.Person.Doc && person.isActive == true)
                .FirstOrDefault<Person>();
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

        public void Remove(string docs)
        {
            var dbPerson = _context.People
                .Where(person => person.Doc == docs && person.isActive == true)
                .FirstOrDefault<Person>();
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
    }
}
