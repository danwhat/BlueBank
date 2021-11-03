using System;
using Infrastructure.Entities;

namespace Infrastructure
{
    internal class Transaction : EntityBase
    {
        public Account AccountFrom { get; set; }
        public Account AccountTo { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public Decimal Value { get; internal set; }
    }
}