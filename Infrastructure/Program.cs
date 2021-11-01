using System;
using Infrastructure.Repositories;

namespace Infrastructure
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new AccountRepository();

            //var now = DateTime.Now;
            //var person = new Domain.Entities.NaturalPerson
            //    { Name = "Alexandre", Cpf = "999999", CreatedAt = now, UpdatedAt = now };
            //var account = new Domain.Entities.Account
            //    { CreatedAt = now, UpdatedAt = now, Person = person };

            //repository.Create(account);
            var result = repository.GetByPersonDocs("999999");

            Console.WriteLine($"conta de {result.Person.Name}: conta numero {result.AccountNumber}");


        }
    }
}
