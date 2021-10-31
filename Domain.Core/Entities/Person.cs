using System.Collections.Generic;

namespace Domain.Entities
{
    public abstract class Person : Entity
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public List<string> PhoneNumbers { get; private set; } = new();
    }
}
 