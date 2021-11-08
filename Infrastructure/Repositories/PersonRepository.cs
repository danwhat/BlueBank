using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces;
using Infrastructure.Shared;

namespace Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly BlueBankContext _context;

        public PersonRepository(BlueBankContext context)
        {
            _context = context;
        }

        #region Interface methods
        public Domain.Entities.Person Update(string doc, Domain.Entities.Person updatePerson)
        {
            int personType = GetPerson.Type(updatePerson);
            if (personType == 0) throw new ServerException(Error.PersonInvalidType);
            
            if (doc != updatePerson.Doc)
            {
                var dbPersonNewDoc = GetPerson.ByDocsOrDefault(updatePerson.Doc, _context);
                if (dbPersonNewDoc != null) throw new ServerException(Error.PersonInvalidDoc);
            }

            var dbPerson = GetPerson.ByDocs(doc, _context);

            dbPerson.Name = updatePerson.Name;
            dbPerson.Address = updatePerson.Address;
            dbPerson.Doc = updatePerson.Doc;
            dbPerson.UpdatedAt = DateTime.Now;
            
            try
            {
                _context.People.Update(dbPerson);
                _context.SaveChanges();

                return (dbPerson.Type == 1)
                    ? BuildInstance.NaturalPerson(dbPerson)
                    : BuildInstance.LegalPerson(dbPerson);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw new ServerException(Error.PersonUpdateFail);
            }
        }

        public Domain.Entities.Person Get(string doc)
        {
            var dbPerson = GetPerson.ByDocsIfActive(doc, _context);
            
            return (dbPerson.Type == 1)
                ? BuildInstance.NaturalPerson(dbPerson)
                : BuildInstance.LegalPerson(dbPerson);
        }

        public Domain.Entities.Person UpdateContactList(string doc, List<string> list)
        {
            var dbPerson = GetPerson.ByDocs(doc, _context);

            try
            {
                _context.Contacts
                    .RemoveRange(
                        _context.Contacts.Where(contact => contact.PersonId == dbPerson.Id));
                
                dbPerson.Contacts = list
                    .Select(phoneNumber => new Contact {
                        Person = dbPerson,
                        PhoneNumber = phoneNumber })
                    .ToList();
                dbPerson.UpdatedAt = DateTime.Now;
                
                _context.People.Update(dbPerson);
                _context.SaveChanges();

                return (dbPerson.Type == 1)
                    ? BuildInstance.NaturalPerson(dbPerson)
                    : BuildInstance.LegalPerson(dbPerson);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw new ServerException(Error.PersonUpdateFail);
            }
        }

        public Domain.Entities.Person RemoveContact(string doc, string phoneNumber)
        {
            var dbPerson = GetPerson.ByDocs(doc, _context);

            try
            {
                var contact = _context.Contacts
                    .Where(curr => curr.PersonId == dbPerson.Id && curr.PhoneNumber == phoneNumber)
                    .FirstOrDefault<Contact>();
                if (contact == null) return (dbPerson.Type == 1)
                    ? BuildInstance.NaturalPerson(dbPerson)
                    : BuildInstance.LegalPerson(dbPerson);

                _context.Contacts.Remove(contact);
                _context.SaveChanges();

                return (dbPerson.Type == 1)
                    ? BuildInstance.NaturalPerson(dbPerson)
                    : BuildInstance.LegalPerson(dbPerson);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new ServerException(Error.PersonUpdateFail);
            }
        }

        public Domain.Entities.Person AddContact(string doc, string phoneNumber)
        {
            var dbPerson = GetPerson.ByDocs(doc, _context);

            try
            {
                var contact = _context.Contacts
                    .Where(contact => contact.PersonId == dbPerson.Id && contact.PhoneNumber == phoneNumber)
                    .FirstOrDefault<Contact>();

                if (contact != null) return (dbPerson.Type == 1)
                   ? BuildInstance.NaturalPerson(dbPerson)
                   : BuildInstance.LegalPerson(dbPerson);

                contact = new Contact { Person = dbPerson, PhoneNumber = phoneNumber };
                _context.Contacts.Add(contact);
                _context.SaveChanges();

                return (dbPerson.Type == 1)
                        ? BuildInstance.NaturalPerson(dbPerson)
                        : BuildInstance.LegalPerson(dbPerson);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw new ServerException(Error.PersonUpdateFail);
            }
        }
        #endregion

        #region Extra methods
        public Domain.Entities.Person Create(Domain.Entities.Person person)
        {
            int personType = GetPerson.Type(person);
            if (personType == 0) return null;

            var dbPerson = GetPerson.ByDocsOrDefault(person.Doc, _context);
            if (dbPerson != null) return null;

            try
            {
                _context.People.Add(new Person
                {
                    Name = person.Name,
                    Doc = person.Doc,
                    Address = person.Address,
                    Type = personType,
                });

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return person;
        }
        #endregion
    }
}
