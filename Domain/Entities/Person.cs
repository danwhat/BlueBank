using Domain.Shared;
using System.Collections.Generic;

namespace Domain
{
    abstract class Person : Entity
    {
        public string Name { get; private set; }
        public string Andress { get; private set; }
        public List<string> PhoneNumbers { get; private set; } = new();
    }
}
 