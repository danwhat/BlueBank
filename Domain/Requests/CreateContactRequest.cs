using Domain.Core.DTOs;
using Domain.Core.Interfaces;
using Domain.Entities;
using Domain.Services.Validations;

namespace Domain.Requests
{
    public class CreateContactRequest
    {
        public CreateContactRequest(
            int accountNumber,
            PersonRequestDto phone,
            IPersonRepository personRepository,
            IAccountRepository accountRepository)
        {
            _accountNumber = accountNumber;
            _phone = phone;
            _accountRepository = accountRepository;
            _personRepository = personRepository;
        }

        private readonly int _accountNumber;
        private readonly PersonRequestDto _phone;
        private readonly IAccountRepository _accountRepository;
        private readonly IPersonRepository _personRepository;

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