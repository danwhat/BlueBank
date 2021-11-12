using System;
using Domain.Core.DTOs;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces;
using Domain.Entities;
using Domain.Services.Validations;

namespace Domain.Requests
{
    public class CreateContactRequest
    {
        private readonly int _accountNumber;

        private readonly ContactDto _contact;

        private readonly IAccountRepository _accountRepository;

        private readonly IPersonRepository _personRepository;

        public CreateContactRequest(
            int accountNumber,
            ContactDto contact,
            IPersonRepository personRepository,
            IAccountRepository accountRepository)
        {
            _accountNumber = accountNumber;
            _contact = contact;
            _accountRepository = accountRepository;
            _personRepository = personRepository;
        }

        public void Validation()
        {
            Validations.PhoneNumberValidation(_contact.PhoneNumber);
            Validations.ThisAccountExistsValidation(_accountRepository, _accountNumber);
            var acc = _accountRepository.Get(_accountNumber);
            Validations.ThisPersonHasNotThatPhoneNumberValidation(_contact.PhoneNumber, acc.Person);
        }

        public ContactResponseDto Create()
        {
            Validation();

            try
            {
                var doc = _accountRepository.Get(_accountNumber).Person.Doc;

                Person currentPerson = _personRepository.AddContact(doc, _contact.PhoneNumber);
                ContactResponseDto response = new(currentPerson, _accountNumber);

                return response;
            }
            catch (ServerException e)
            {
                throw new Exception("Ocorreu um erro: " + e.Message);
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro interno.");
            }
        }
    }
}