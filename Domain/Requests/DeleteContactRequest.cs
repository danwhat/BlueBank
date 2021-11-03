﻿using Domain.Core.DTOs;
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

        public Person Delete()
        {
            Person result = _personRepository.RemoveContact(_doc, _phoneNumber);
            return result;
        }
    }
}