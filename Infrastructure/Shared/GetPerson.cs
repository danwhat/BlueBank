using Domain.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace Infrastructure.Shared
{
    internal static class GetPerson
    {
        internal static Person ByDocs(string docs, BlueBankContext context)
        {
            Person dbPerson = null;
            try
            {
                dbPerson = context.People
                    .Where(curr => curr.Doc == docs)
                    .Include(person => person.Contacts)
                    .FirstOrDefault<Person>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw new ServerException(Error.PersonGetFail);
            }

            if (Validate.IsNull(dbPerson)) throw new ServerException(Error.PersonNotFound);
            return dbPerson;
        }

        internal static Person ByDocsIfActive(string docs, BlueBankContext context)
        {
            Person dbPerson = null;
            try
            {
                dbPerson = context.People
                    .Where(curr => curr.Doc == docs && curr.IsActive == true)
                    .Include(person => person.Contacts)
                    .FirstOrDefault<Person>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw new ServerException(Error.PersonGetFail);
            }
            
            if (Validate.IsNull(dbPerson)) throw new ServerException(Error.PersonNotFound);
            return dbPerson;

        }

        internal static Person ByDocsOrDefault(string docs, BlueBankContext context)
        {
            try
            {
                return context.People
                .Where(curr => curr.Doc == docs)
                .Include(person => person.Contacts)
                .FirstOrDefault<Person>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new Person();
            }
        }

        internal static int Type(Domain.Entities.Person person)
        {
            if (person.GetType() == typeof(Domain.Entities.NaturalPerson)) return 1;
            if (person.GetType() == typeof(Domain.Entities.LegalPerson)) return 2;
            return 0;
        }
    }
}
