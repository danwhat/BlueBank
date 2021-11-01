﻿using System.Linq;
using Domain.Core.DTOs;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class AccountRepository
    {
        private readonly BlueBankContext _context;

        public AccountRepository()
        {
            _context = new BlueBankContext();
        }

        public Domain.Entities.Account GetByPersonDocs(string docs)
        {
            // validacoes
            var person = _context.People.Where(person => person.Doc == docs).First<Person>();
            var account = _context.Accounts.Where(account => account.PersonId == person.Id).First<Account>();

            var currentBalance = account.Balances?.OrderByDescending(item => item.CreatedAt).First();

            var accountEntity = new Domain.Entities.Account 
            { AccountNumber = account.Id, Balance = (currentBalance == null) ? 0 : currentBalance.Value };

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
            // tipo = 1 pf 2 pj
            string docs = "";
            int type = 0;

            if (account.Person.GetType() == typeof(NaturalPerson))
            {
                var getPerson = (NaturalPerson)account.Person;
                docs = getPerson.Cpf;
                type = 1;
            } 
            else if (account.Person.GetType() == typeof(LegalPerson))
            {
                var getPerson = (LegalPerson)account.Person;
                docs = getPerson.Cnpj;
                type = 2;
            }

            if (type > 0)
            {
                if (account.Person.Id > 0)
                {
                    var dbPerson = new Person() { Name = account.Person.Name, Doc = docs, Type = type, Id = account.Person.Id };
                    var dbAccount = new Account() { Person = dbPerson };
                    var result = _context.Accounts.Add(dbAccount);

                    _context.SaveChanges();

                    account.AccountNumber = dbAccount.Id;

                    return account;
                }
                else
                {
                    var dbPerson = new Person() { Name = account.Person.Name, Doc = docs, Type = type };
                    var dbAccount = new Account() { Person = dbPerson };
                    
                    _context.People.Add(dbPerson);
                    _context.Accounts.Add(dbAccount);

                    _context.SaveChanges();

                    account.AccountNumber = dbAccount.Id;
                    // account.Person.Id = dbPerson.Id; LEMBRAR SE O SERVICES PRECISA DO ID DE CLIENTE NOVO CRIADO

                    return account;
                }
            }

            return null;

        }
    }
}