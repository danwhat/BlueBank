using System.Linq;

namespace Infrastructure.Shared
{
    internal static class GetPerson
    {
        internal static Person ByDocs(string docs, BlueBankContext context)
        {
            var person = context.People
                .Where(curr => curr.Doc == docs)
                .FirstOrDefault<Person>();

            var contactList = context.Contacts
                .Where(contact => contact.PersonId == person.Id)
                .ToList();

            person.Contacts = contactList;

            return person;
        }

        internal static Person IfActive(string docs, BlueBankContext context)
        {
            var person = context.People
                .Where(curr => curr.Doc == docs && curr.IsActive == true)
                .FirstOrDefault<Person>();

            var contactList = context.Contacts
                .Where(contact => contact.PersonId == person.Id)
                .ToList();

            person.Contacts = contactList;

            return person;

        }
    }
}
