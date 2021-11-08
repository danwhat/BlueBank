using System;
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

        public DeleteContactRequest(string doc, string phoneNumber, IPersonRepository personRepository)
        {
            _phoneNumber = phoneNumber;
            _doc = doc;
            _personRepository = personRepository;
        }



        public void Valid()
        {
            //Person person = (Person)Activator.CreateInstance(typeof(Person));
            Validations.ThisPersonExistsValidation(_personRepository, _doc, out Person person);
            Validations.ThisPersonHasThatPhoneNumberValidation(_phoneNumber, person);
        }

        public Person Delete()
        {
            Valid();
            Person result = _personRepository.RemoveContact(_doc, _phoneNumber);
            return result;
        }
    }
}
