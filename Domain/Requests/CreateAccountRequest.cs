using Domain.Core.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Domain.Requests
{
    public class CreateAccountRequest
    {
        private readonly Person Person;
        private readonly Account account;

        public CreateAccountRequest(NewAccountDto dto)
        {
            Person = new NaturalPerson() { Name = dto.Name, Cpf = dto.Doc };
            Account = new Account() { Person = this.Person };
        }

        public bool Validate()
        {
            // validadoes de docs
            // validadoes de name
            return true;
        }

        public NewAccountDto Create()
        {
            // mandar repositorio da infra criar conta
            // verificar se a pessoa existe
            // verificar se a pessoa já tem conta
            // se ok, criar conta

            var repository = new AccountRepository();

            var result = repository.Create(_dto);

            return result;
        }
    }
}
