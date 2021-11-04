using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Core.DTOs
{
    public class PersonResponseDto
    {
        public PersonResponseDto()
        {
        }

        public PersonResponseDto(Person person, int accountNumber)
        {
            AccountNumber = accountNumber;
            PhoneNumber = person.PhoneNumbers;
        }

        public int AccountNumber { get; set; }

        public List<string> PhoneNumber { get; set; }
    }
}
