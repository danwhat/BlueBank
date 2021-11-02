using System;
using Infrastructure.Entities;

namespace Infrastructure
{
    internal class Balance : EntityBase
    {
        public Decimal Value { get; internal set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public DateTime CreatedAt { get; internal set; }
    }
}
