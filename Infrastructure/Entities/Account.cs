using System;
using System.Collections.Generic;
using Infrastructure.Entities;

namespace Infrastructure
{
    internal class Account : EntityBase
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public ICollection<TransactionLog> TransactionLogs { get; set; }
        public ICollection<Transaction> TransactionsFrom { get; set; }
        public ICollection<Transaction> TransactionsTo { get; set; }
        public bool? IsActive { get; set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}