using System;

namespace Infrastructure
{
    internal class Person
    {
        public int Id { get; set; }
        public string Name { get; internal set; }
        public int Tipo { get; internal set; }
        public string Doc { get; internal set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}