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
    public class DeleteAccountRequest
    {
        private readonly int _accountNumber;
        private readonly AccountRepository _accountRepository;

        public DeleteAccountRequest(int accountNumber)
        {
            _accountNumber = accountNumber;
            _accountRepository = new AccountRepository();
        }
        

        public void Validation()
        {
            Validations.ThisAccountExistsValidation(_accountRepository, _accountNumber);
        }

        public bool Delete()
        {
            Validation();
            Account acc = _accountRepository.Get(_accountNumber);
            var account = new Infrastructure.Account();
            bool result = _accountRepository.Delete(acc);
            return result;
        }
    }
}
