using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.DTOs
{
    public class TransactionDTO
    {
        public Decimal Value { get; set; }
        public int AccountNumberTo { get; set; }
    }
}
