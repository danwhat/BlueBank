using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    class Transaction : Entity
    {
        public Account AccountFrom { get; set; }
        public Account AccountTo { get; set; }
        public decimal Value { get; private set; }

    }
}
