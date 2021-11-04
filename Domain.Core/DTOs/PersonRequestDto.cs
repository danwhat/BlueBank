using Domain.Entities;

namespace Domain.Core.DTOs
{
    public class PersonRequestDto
    {
        public PersonRequestDto()
        {
        }

        public PersonRequestDto(Account acc)
        {
            AccountNumber = acc.AccountNumber;
            PhoneNumber = acc.Person.PhoneNumbers[0];
        }

        public int AccountNumber { get; set; }

        public string PhoneNumber { get; set; }
    }
}
