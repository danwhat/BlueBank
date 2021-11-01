using System.Collections.Generic;

namespace Domain.Entities
{
    public abstract class Person : Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<string> PhoneNumbers { get; private set; } = new();
    }
}
 