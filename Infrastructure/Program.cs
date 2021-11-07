﻿using Infrastructure.Repositories;

namespace Infrastructure
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BlueBankContext();
            var peopleRepository = new PersonRepository(context);
            var accountRepository = new AccountRepository(context);
            var transactionRepository = new TransactionRepository(context);

            //var now = DateTime.Now;

            // criação de user
            var person = new Domain.Entities.LegalPerson
            { Name = "Daniel Madson Inc", Cnpj = "456", Address = "Vizinho a casa do Dan" };

            //var person2 = new Domain.Entities.NaturalPerson
            //{ Name = "Alexandre Segundus", Cpf = "888", Address = "Rua de Trás da Vila Xp" };
            //var newPerson = peopleRepository.Create(person);

            // add contact
            //var personWithContact = peopleRepository.AddContact("456", "tomar uma");

            // pegar user
            //var teste = peopleRepository.Get("456");

            //update user
            //person.Cnpj ="123";
            //var result2 = peopleRepository.Update("123", person);


            //person.Cpf = "123";

            // update contact list
            //var list = new List<string>
            //{
            //    "83 1234",
            //    "aooooooo",
            //    "vai Disgrama!"
            //};
            //var listResult = peopleRepository.UpdateContactList("123", list);

            // remove contact
            //var retorno = peopleRepository.RemoveContact("123", "vai Disgrama!");


            //var teste = peopleRepository.Get("123");

            //criar conta
            var newAccount = new Domain.Entities.Account { Person = person };
            var resultAccount = accountRepository.Create(newAccount);

            // get account
            //var result = accountRepository.Get(2);
            //var result = accountRepository.Get(0);
            //var result = accountRepository.Get("123");


            //remove account
            //var account = new Domain.Entities.Account { AccountNumber = 1 };
            //var result = accountRepository.Delete(account);
            //var resultAccount2 = accountRepository.Get(1);

            //Console.WriteLine($"Conta removida? {((result) ? "sim" : "não")}");
            //Console.WriteLine(updated.PhoneNumbers);

            //var transaction = new Domain.Entities.Transaction()
            //{
            //    AccountTo = result,
            //};

            //transaction.SetValue(250.00m);

            //transactionRepository.Create(transaction);
            
        }
    }
}
