using System;
using Domain.Core.DTOs;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repositories;

namespace Domain.Requests
{
    public class CreateAccountRequest
    {
        public CreateAccountRequest(AccountDto dto)
        {
            _dto = dto;
            //_accountRepository = new AccountRepository(context);
        }

        private readonly AccountDto _dto;
        private readonly AccountRepository _accountRepository;

        public bool Validate()
        {
            // validadoes de docs
            // validadoes de name
            return true;
        }
        
        public AccountDto Create()
        {
            if (!Validate()) throw new Exception("Faltaou tal coisa aqui");

            var account = new Account();
            
            if (isNaturalPerson())
            {
                NaturalPerson person = new ();
                person.Name = _dto.Name;
                person.Address = _dto.Address;
                person.Cpf = _dto.Doc;
                person.PhoneNumbers.Add(_dto.PhoneNumber);

                account.Person = person;
            }
            else
            {
                LegalPerson person = new ();
                person.Name = _dto.Name;
                person.Address = _dto.Address;
                person.Cnpj = _dto.Doc;
                person.PhoneNumbers.Add(_dto.PhoneNumber);

                account.Person = person;
            }

            Account newAccount = _accountRepository.Create(account);
            AccountDto response = new(newAccount);
            return response;
        }

        public bool isNaturalPerson()
        {
            return _dto.Doc.Length == 11;
        }
    }
}
