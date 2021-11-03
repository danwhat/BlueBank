using System;
using System.Collections.Generic;
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
            // criação de user
            var person = new Domain.Entities.NaturalPerson
            { Name = "Alexandre Leite", Cpf = "123", Address = "Rua da Vila Xp" };
            var newPerson = peopleRepository.Create(person);

            // add contact
            //peopleRepository.AddContact("123", "aloooooou767676");

            // pegar user
            //var teste = peopleRepository.Get("123");

            //update user
            //var result2 = peopleRepository.Update("123", person);


            //person.Cpf = "123";

            // update contact list
            //var list = new List<string>
            //{
            //    "83 1234",
            //    "aooooooo",
            //    "vai Disgrama!"
            //};
            //var result = peopleRepository.UpdateContactList("123", list);

            // remove contact
            //var retorno = peopleRepository.RemoveContact("123", "vai Disgrama!");


            var teste = peopleRepository.Get("123");

            // get account
            //var result = accountRepository.Get(1);
            //var result = accountRepository.Get(0);
            //var result = accountRepository.Get("123");

            //criar conta
            var newAccount = new Domain.Entities.Account { Person = person };
            var resultAccount = accountRepository.Create(newAccount);
            
            //remove account
            //var account = new Domain.Entities.Account { AccountNumber = 1 };
            //var result = accountRepository.Delete(account);

            //Console.WriteLine($"Conta removida? {((result) ? "sim" : "não")}");
            //Console.WriteLine(updated.PhoneNumbers);
        }
    }
}
