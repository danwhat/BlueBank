using Domain.Core.DTOs;
using Domain.Entities;
using Domain.Services.Validations;
using Infrastructure.Repositories;
using System;

namespace Domain.Requests
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

        public void Validation()
        {
            Validations.PhoneNumberValidation(_phone.PhoneNumber);
            Validations.ThisAccountExistsValidation(_accountRepository, _accountNumber);
            var acc = _accountRepository.Get(_accountNumber);
            Validations.ThisPersonHasNotThatPhoneNumberValidation(_phone.PhoneNumber, acc.Person);
        }

        public PersonResponseDto Create()
        {
            Validation();

            var doc = _accountRepository.Get(_accountNumber).Person.Doc;

            Person currentPerson = _personRepository.AddContact(doc, _phone.PhoneNumber);
            PersonResponseDto response = new(currentPerson, _accountNumber);

            return response;
        }
    }
}