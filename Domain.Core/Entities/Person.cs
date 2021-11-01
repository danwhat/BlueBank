using System.Collections.Generic;

namespace Domain.Entities
{
    public abstract class Person : Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Doc { get; protected set; }
        public List<string> PhoneNumbers { get; set; } = new();
    }
}
 