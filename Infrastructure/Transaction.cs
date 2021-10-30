using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    class Transaction
    {
        public int Id { get; set; }
        public Decimal Value { get; internal set; }

        public int AccountFromId { get; set; }
        public Account AccountFrom { get; set; }
        public DateTime CreatedAt { get; internal set; }
    }
}
