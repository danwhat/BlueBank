using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure
{
    internal class Account
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
        
        public List<Balance> Balances { get; set; }

        //[InverseProperty("AccountFrom")]
        //public List<Transaction> OutcomingTransactions { get; set; }

        //[InverseProperty("AccountTo")]
        //public List<Transaction> IngoingTransactions { get; set; }

        //public List<Transaction> Transactions { get; set; }

        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}
