using System;
using Domain.Core.DTOs;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repositories;

namespace Domain.Requests
{
    public class CreateAccountRequest
    {
        public CreateAccountRequest(NewAccountDto dto, BlueBankContext context)
        {
            _dto = dto;
            _accountRepository = new AccountRepository(context);
        }

        private readonly NewAccountDto _dto;
        private readonly AccountRepository _accountRepository;

        public bool Validate()
        {
            // validadoes de docs
            // validadoes de name
            return true;
        }
        
        public NewAccountDto Create()
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
            
            Account result = _accountRepository.Create(account);
            NewAccountDto response = new ();
            response.AccountNumber = result.AccountNumber;
            response.Address = result.Person.Address;
            if (isNaturalPerson())
            {
                NaturalPerson pn = (NaturalPerson)result.Person;
                response.Doc = pn.Cpf;
            }
            else
            {
                LegalPerson pn = (LegalPerson)result.Person;
                response.Doc = pn.Cnpj;
            }
            response.Name = result.Person.Name;
            response.PhoneNumber = result.Person.PhoneNumbers[0];
            return response;
        }

        public bool isNaturalPerson()
        {
            return _dto.Doc.Length == 11;
        }
    }
}
