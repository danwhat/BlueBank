using System;
using System.Linq;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class PeopleRepository
    {
        private readonly BlueBankContext _context;

        public PeopleRepository(BlueBankContext context)
        {
            _context = context;
        }

        public Domain.Entities.Person GetByDoc(string docs)
        {
            // validacoes
            var person = GetPersonInDB(docs);

            if (person.Type == 1)
            {
                var personEntity = new Domain.Entities.NaturalPerson
                    { Cpf = person.Doc, Name = person.Name, Address = person.Address };
                return personEntity;
            }
            else
            {
                var personEntity = new Domain.Entities.LegalPerson
                    { Cnpj = person.Doc, Name = person.Name, Address = person.Address };
                return personEntity;
            }

        }
        public Domain.Entities.Person Create(Domain.Entities.Person person)
        {
            //int personType = 0;
            //if (person.GetType() == typeof(NaturalPerson)) personType = 1;
            //if (person.GetType() == typeof(LegalPerson)) personType = 2;

            //if (personType == 0) return null;

            //var dbPerson = _context.People.Where(curr => curr.Doc == person.Doc).FirstOrDefault<Person>();

            //if (dbPerson.Id != 0) throw new Exception();

            //var result = _context.People.Add(new Person
            //{
            //    Name = person.Name,
            //    Doc = person.Doc,
            //    Type = personType,
            //    Address = person.Address,
            //    CreatedAt = DateTime.Now,
            //    UpdatedAt = DateTime.Now,
            //});
            
            //_context.SaveChanges();

            //person.Id = dbPerson.Id;
            return person;
        }
        public Domain.Entities.Person Update(Domain.Entities.Person person, string docs)
        {
            int personType = 0;
            if (person.GetType() == typeof(NaturalPerson)) personType = 1;
            if (person.GetType() == typeof(LegalPerson)) personType = 2;

            if (personType == 0) return null;

            var dbPerson = GetPersonInDB(docs);

            if (dbPerson == null) throw new Exception();

            dbPerson.Name = person.Name;
            dbPerson.Doc = person.Doc;
            dbPerson.Address = person.Address;
            dbPerson.UpdatedAt = DateTime.Now;

            var result = _context.People.Update(dbPerson);

            _context.SaveChanges();

            if (personType == 1)
            {
                var personEntity = new Domain.Entities.NaturalPerson
                { 
                    Cpf = dbPerson.Doc,
                    Name = dbPerson.Name,
                    Address = dbPerson.Address,
                    CreatedAt = dbPerson.CreatedAt, 
                    UpdatedAt = dbPerson.UpdatedAt
                };
                return personEntity;
            }
            else
            {
                var personEntity = new Domain.Entities.LegalPerson
                {
                    Cnpj = dbPerson.Doc,
                    Name = dbPerson.Name,
                    Address = dbPerson.Address,
                    CreatedAt = dbPerson.CreatedAt,
                    UpdatedAt = dbPerson.UpdatedAt
                };
                return personEntity;
            }
        }
        public void Remove(string docs)
        {
            var isActive = false;
            try
            {
                var dbPerson = GetPersonInDB(docs);
                if (dbPerson == null) throw new Exception();
                ChangeStatus(dbPerson, isActive);
                var dbAccount = RemoveAccount(dbPerson);
                _context.People.Update(dbPerson);
                if (dbAccount != null) _context.Accounts.Update(dbAccount);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Reactivate(string docs)
        {
            var isActive = true;
            try
            {
                var dbPerson = GetPersonInDB(docs);
                if (dbPerson == null) throw new Exception();
                ChangeStatus(dbPerson, isActive);
                _context.People.Update(dbPerson);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void AddContact(Domain.Entities.Person person, string newContact)
        {
            var dbPerson = GetPersonInDB(person.Doc);
            if (dbPerson == null) throw new Exception();

            var dbContact = _context.Contacts
                .Where(curr => curr.PersonId == dbPerson.Id && curr.PhoneNumber == newContact)
                .FirstOrDefault<Contact>();
            if (dbContact != null) return;

            dbContact = new Contact { Person = dbPerson, PhoneNumber = newContact };
            try
            {
                _context.Contacts.Add(dbContact);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void RemoveContact(Domain.Entities.Person person, string contact)
        {
            var dbPerson = GetPersonInDB(person.Doc);
            if (dbPerson == null) throw new Exception();

            var dbContact = _context.Contacts
                .Where(curr => curr.PersonId == dbPerson.Id && curr.PhoneNumber == contact)
                .FirstOrDefault<Contact>();
            if (dbContact != null) return;

            try
            {
                _context.Contacts.Remove(dbContact);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private Person ChangeStatus(Person person, bool status)
        {
            person.isActive = status;
            return person;
        }
        private Account RemoveAccount(Person person)
        {
            var dbAccount = _context.Accounts
                .Where(curr => curr.PersonId == person.Id && curr.IsActive == true)
                .FirstOrDefault<Account>();
            if (dbAccount != null) dbAccount.IsActive = false;

            return dbAccount;
        }
        private Person GetPersonInDB(string docs)
        {
            return _context.People
                .Where(curr => curr.Doc == docs)
                .FirstOrDefault<Person>();
        }
    }
}
