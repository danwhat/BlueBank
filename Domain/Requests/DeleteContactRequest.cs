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
        private readonly ContactRepository _contactRepository;

        public DeleteContactRequest(string doc, string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            _doc = doc;
            _contactRepository = new ContactRepository();
        }

        public Person Delete()
        {
            Person result = _contactRepository.RemoveContact(_doc, _phoneNumber);
            return result;
        }
    }
}
