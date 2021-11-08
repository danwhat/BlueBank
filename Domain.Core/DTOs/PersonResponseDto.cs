using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Core.DTOs
{
    public class PersonResponseDto
    {
        public PersonResponseDto()
        {
        }

        // TODO: Testar
        // person null
        // person not null
        // person not Null c/ PhoneNumbers null
        // person not Null c/ PhoneNumbers null
        // accountNumber Null
        // accountNumber not Null
        public PersonResponseDto(Person person, int accountNumber)
        {
            AccountNumber = accountNumber;
            PhoneNumber = person?.PhoneNumbers;
        }

        public int AccountNumber { get; set; }

        public List<string> PhoneNumber { get; set; }
    }
}
