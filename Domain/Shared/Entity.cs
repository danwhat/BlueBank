using System;

namespace Domain.Shared
{
    class Entity
    {
        public Guid id { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
