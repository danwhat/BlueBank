using Infrastructure.Entities;

namespace Infrastructure
{
    internal class Transaction : EntityBase
    {
        public int? AccountFromId { get; set; }
        public Account AccountFrom { get; set; }
        public int? AccountToId { get; set; }
        public Account AccountTo { get; set; }
        public decimal Value { get; internal set; }
    }
}