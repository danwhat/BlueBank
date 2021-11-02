using System;
using System.Collections.Generic;
using Infrastructure.Entities;

namespace Infrastructure
{
    internal class Account : EntityBase
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public List<Balance> Balances { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}
