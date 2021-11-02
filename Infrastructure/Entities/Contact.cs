using System;
using Infrastructure.Entities;

namespace Infrastructure
{
    internal class Contact : EntityBase
    {
        public string PhoneNumber { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public DateTime CreatedAt => DateTime.Now;
    }
}
