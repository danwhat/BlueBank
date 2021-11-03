using Domain.Core.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using System;

namespace Domain.Requests
{
    public class UpdateAccountRequest
    {
        public UpdateAccountRequest(AccountDto dto)
        {
            _dto = dto;
            _accountRepository = new AccountRepository();
        }

        private readonly AccountDto _dto;
        private readonly AccountRepository _accountRepository;

        public bool Validate()
        {
            // valida se a conta existe
            Account acc = _accountRepository.Get(_dto.AccountNumber);
            if (acc != null) return false;
            return true;
        }
        
        public AccountDto Update()
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
            
            Account updatedAccount = _accountRepository.Update(account);
            AccountDto response = new (updatedAccount);
            return new AccountDto();
        }

        public bool isNaturalPerson()
        {
            return _dto.Doc.Length == 11;
        }
    }
}
