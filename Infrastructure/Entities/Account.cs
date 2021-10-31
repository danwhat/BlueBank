using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class Account
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public List<Balance> Balances { get; set; }

        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}
