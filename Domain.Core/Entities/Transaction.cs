namespace Domain.Entities
{
    public class Transaction : Entity
    {
        public Account AccountFrom { get; set; }
        public Account AccountTo { get; set; }
        public decimal Value { get; private set; }

    }
}
