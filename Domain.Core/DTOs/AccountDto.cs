using System.Collections.Generic;
using Domain.Entities;

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
            Address = acc.Person.Address;
            PhoneNumbers = acc.Person.PhoneNumbers;
            Balance = acc.Balance;
        }

        public int AccountNumber { get; set; }
        public string Doc { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public decimal Balance { get; private set; }
    }
}