using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.DTOs
{
    public class AccountDto
    {
        public AccountDto()
        {
        }

        public AccountDto(Account acc)
        {
            AccountNumber = acc.AccountNumber;
            Doc = acc.Person.Doc;
            Name = acc.Person.Name;
            PhoneNumber = acc.Person.PhoneNumbers[0];
        }

        public int AccountNumber { get; set; }
        public string Doc { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
