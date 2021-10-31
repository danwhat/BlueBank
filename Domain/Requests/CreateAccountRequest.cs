using Domain.Core.DTOs;
using Infrastructure.Repositories;

namespace Domain.Requests
{
    public class CreateAccountRequest
    {
        private readonly NewAccountDto _dto;

        public CreateAccountRequest(NewAccountDto dto)
        {
            _dto = dto;
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
