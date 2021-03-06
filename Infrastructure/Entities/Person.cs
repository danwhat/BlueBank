using System;
using System.Collections.Generic;
using Infrastructure.Entities;

namespace Infrastructure
{
    internal class Person : EntityBase
    {
        public string Name { get; internal set; }
        public string Address { get; internal set; }
        public int Type { get; internal set; }
        public string Doc { get; internal set; }
        public ICollection<Account> Account { get; set; }
        public ICollection<Contact> Contacts { get; set; }        
        public DateTime UpdatedAt { get; internal set; }
        public bool IsActive { get; set; }

    }
}