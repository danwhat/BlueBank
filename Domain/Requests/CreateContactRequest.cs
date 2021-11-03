using Domain.Core.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    public class CreateContactRequest
    {
        private readonly NewContactDto _dto;
        private readonly AccountRepository _accountRepository;

        public CreateContactRequest(NewContactDto dto)
        {
            _dto = dto;
            _accountRepository = new AccountRepository();
        }

        public bool Validate()
        {
            return true;
        }

        public NewContactDto Create()
        {
            if (!Validate()) throw new Exception("Faltou o contato.");

            var account = new Account();

            if (isNaturalPerson())
            {
                NaturalPerson person = new();
                person.PhoneNumbers.Add(_dto.PhoneNumber);

                account.Person = person;
            }
            else
            {
                LegalPerson person = new();
                person.PhoneNumbers.Add(_dto.PhoneNumber);

                account.Person = person;
            }

            Account result = _accountRepository.Create(account);
            NewContactDto response = new();
            response.AccountNumber = result.AccountNumber;
            response.Doc = result.Person.Doc;
            response.PhoneNumber = result.Person.PhoneNumbers[1];
            return response;
        }

        public bool isNaturalPerson()
        {
            return _dto.Doc.Length == 11;
        }
    }
}
