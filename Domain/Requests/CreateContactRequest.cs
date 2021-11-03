using Domain.Core.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using System;

namespace Domain.Services.Requests
{
    public class CreateContactRequest
    {
        public CreateContactRequest(int accountNumber, PersonRequestDto phone)
        {
            _accountNumber = accountNumber;
            _phone = phone;
            _accountRepository = new AccountRepository();
            _personRepository = new PersonRepository();
        }

        private readonly int _accountNumber;
        private readonly PersonRequestDto _phone;
        private readonly AccountRepository _accountRepository;
        private readonly PersonRepository _personRepository;

        public bool Validate()
        {
            return true;
        }
        
        public PersonResponseDto Create()
        {
            if (!Validate()) throw new Exception("Algo de errado");

            var doc = _accountRepository.Get(_accountNumber).Person.Doc;

            Person currentPerson = _personRepository.AddContact(doc, _phone.PhoneNumber);
            PersonResponseDto response = new(currentPerson, _accountNumber);

            return response;
        }
    }
}
