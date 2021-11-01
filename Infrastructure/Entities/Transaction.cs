using System;

namespace Infrastructure
{
    internal class Transaction
    {
        public Account AccountFrom { get; set; }
        public Account AccountTo { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public int Id { get; set; }
        public Decimal Value { get; internal set; }
    }
}