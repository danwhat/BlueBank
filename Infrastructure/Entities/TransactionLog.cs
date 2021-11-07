using System;
using Infrastructure.Entities;

namespace Infrastructure
{
    internal class TransactionLog : EntityBase
    {
        public decimal Value { get; internal set; }
        public decimal BalanceAfter { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }        
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
