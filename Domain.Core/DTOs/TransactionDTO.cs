﻿using System;

namespace Domain.Core.DTOs
{
    public class TransactionDto
    {
        public Decimal Value { get; set; }
        public int AccountNumberTo { get; set; }
    }
}