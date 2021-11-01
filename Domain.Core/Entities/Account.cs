using System;

namespace Domain.Entities
{
    public class Account : Entity
    {
        public Person Person { get; set; }
        public int AccountNumber { get; set; }
        public Decimal Balance { get; set; }
    }
}
 