using System.Linq;
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
    }
}
