using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure
{
    internal class Account
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        
        public List<Balance> Balances { get; set; }

        [InverseProperty("AccountFrom")]
        public List<Transaction> IncomingTransactions { get; set; }

        [InverseProperty("AccountTo")]
        public List<Transaction> OutgoingTransactions { get; set; }

        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}
