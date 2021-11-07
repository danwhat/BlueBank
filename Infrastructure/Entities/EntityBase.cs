using System;

namespace Infrastructure.Entities
{
    internal abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; internal set; }
    }
}
