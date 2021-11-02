using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.DTOs
{
    public class TransactionResponseDTO
    {
        public string Message { get; set; }
        public Decimal OldBalance { get; set; }
        public Decimal CurrentBalance { get; set; }
    }
}
