using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Core.DTOs
{
    public class ContactResponseDto
    {
        public ContactResponseDto()
        {
        }

        /* TODO:
        Testar person null; person not null; person not Null c/ PhoneNumbers null; person not
        Null; c/ PhoneNumbers null accountNumber Null accountNumber not Null
        */

        public ContactResponseDto(Person person, int accountNumber)
        {
            AccountNumber = accountNumber;
            PersonName = person.Name;
            PersonDoc = person.Doc;
            PhoneNumbers = person?.PhoneNumbers;
        }

        public int AccountNumber { get; set; }

        public string PersonName { get; set; }

        public string PersonDoc { get; set; }

        public List<string> PhoneNumbers { get; set; }
    }
}