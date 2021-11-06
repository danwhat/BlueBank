using Domain.Core.DTOs;
using Domain.Core.Exceptions;
using Domain.Entities;
using Domain.Services.Validations;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    public class DeleteContactRequest
    {
        private readonly string _phoneNumber;
        private readonly string _doc;
        private readonly PersonRepository _personRepository;

        public DeleteContactRequest(string doc, string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            _doc = doc;
            _personRepository = new PersonRepository();
        }



        public void valid()
        {
            Person person = (Person)Activator.CreateInstance(typeof(Person));
            Validations.ThisPersonExistsValidation(_personRepository, _doc, out person);
            Validations.ThisPersonHasThatPhoneNumberValidation(_phoneNumber, person);
        }

        public Person Delete()
        {
            valid();
            Person result = _personRepository.RemoveContact(_doc, _phoneNumber);
            return result;
        }
    }
}
