using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
