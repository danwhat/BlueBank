using System;
using Infrastructure.Entities;

namespace Infrastructure
{
    internal class Transaction : EntityBase
    {
        public Account AccountFrom { get; set; }
        public Account AccountTo { get; set; }        
        public decimal Value { get; internal set; }
    }
}