using Domain.Entities;

namespace Domain.Core.DTOs
{
    public class CreateAccountDto
    {
        public CreateAccountDto()
        {
        }

        public CreateAccountDto(Account acc)
        {
            Doc = acc.Person.Doc;
            Name = acc.Person.Name;
            Address = acc.Person.Address;
            PhoneNumber = acc.Person.PhoneNumbers[0];
        }

        public string Doc { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}