using System;
using Infrastructure.Repositories;

namespace Infrastructure
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BlueBankContext();
            var peopleRepository = new PeopleRepository(context);
            var accountRepository = new AccountRepository(context);

            var now = DateTime.Now;
            var person = new Domain.Entities.NaturalPerson
            { Name = "Alexandre Leite", Cpf = "456", Address = "Rua da Vila Xp", CreatedAt = now, UpdatedAt = now };

            var result = peopleRepository.Create(person);
            //peopleRepository.AddContact(result, "aloooooou767676");
            //var updated = peopleRepository.GetByDoc("456");

            Console.WriteLine($"Doc de {result.Name} é {result.Doc}");
            //Console.WriteLine(updated.PhoneNumbers);


        }
    }
}
