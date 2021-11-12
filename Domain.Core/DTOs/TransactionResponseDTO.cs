using System;

namespace Domain.Core.DTOs
{
    public class TransactionResponseDto
    {
        public string Message { get; set; }
        public Decimal OldBalance { get; set; }
        public Decimal CurrentBalance { get; set; }
    }
}