using System;
using System.Collections.Generic;
using Infrastructure.Entities;

namespace Infrastructure
{
    internal class Account : EntityBase
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public List<TransactionLog> TransactionLogs { get; set; }
        public List<Transaction> TransactionsFrom { get; set; }
        public List<Transaction> TransactionsTo { get; set; }
        public bool? IsActive { get; set; }        
        public DateTime UpdatedAt { get; internal set; }

    }
}
