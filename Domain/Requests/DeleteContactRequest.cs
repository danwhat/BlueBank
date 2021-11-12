using System;
using Domain.Core.DTOs;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces;
using Domain.Entities;
using Domain.Services.Validations;

namespace Domain.Requests
{
    public class DeleteContactRequest
    {
        private readonly string _phoneNumber;
        private readonly string _doc;
        private readonly IPersonRepository _personRepository;
        private readonly IAccountRepository _accountRepostory;

        public DeleteContactRequest(
            string doc,
            string phoneNumber,
            IPersonRepository personRepository,
            IAccountRepository accountRepostory)
        {
            _phoneNumber = phoneNumber;
            _doc = doc;
            _personRepository = personRepository;
            _accountRepostory = accountRepostory;
        }

        public void Valid()
        {
            //Person person = (Person)Activator.CreateInstance(typeof(Person));
            Validations.ThisPersonExistsValidation(_personRepository, _doc, out Person person);
            Validations.ThisPersonHasThatPhoneNumberValidation(_phoneNumber, person);
        }

        public ContactResponseDto Delete()
        {
            Valid();
            try
            {
                var account = _accountRepostory.Get(_doc);
                Person result = _personRepository.RemoveContact(_doc, _phoneNumber);
                ContactResponseDto response = new(result, account.AccountNumber);

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