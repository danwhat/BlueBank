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

        public bool IsPersonExist(Person person)
        {
            return person != null;
        }

        private bool IsPhoneNumberExist(string phoneNumber, Person person)
        {
            return person.PhoneNumbers.Contains(phoneNumber);
        }

        public Person Delete()
        {
            Person person = _personRepository.Get(_doc);
            if (!IsPersonExist(person)) throw new Exception("Não existe nenhuma pessoa cadastrada com esse documento!");
            if (!IsPhoneNumberExist(_phoneNumber, person)) throw new Exception("Esse número de telefone não está cadastrado");

            Person result = _personRepository.RemoveContact(_doc, _phoneNumber);
            return result;
        }
    }
}
