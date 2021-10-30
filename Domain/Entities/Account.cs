using Domain.Shared;
using System;

namespace Domain.Entities
{
    class Account : Entity
    {
        public Person Person { get; private set; }
        public int AccountNumber { get; }
        public Decimal Balance { get; private set; }

        public Account(Person person, int accountNumber, decimal value)
        {
            Person = person;
            AccountNumber = accountNumber;
            Balance = value;
        }
    }
}
 