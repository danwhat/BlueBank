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
    public class DeleteAccountRequest
    {
        private readonly int _accountNumber;
        private readonly AccountRepository _accountRepository;

        public DeleteAccountRequest(int accountNumber)
        {
            _accountNumber = accountNumber;
            _accountRepository = new AccountRepository();
        }

        public bool Validate(Account acc)
        {
            if (acc != null) return false;
            return true;
        }

        public bool Delete()
        {
            Account acc = _accountRepository.Get(_accountNumber);
            if (!Validate(acc)) throw new Exception("Erro ao deletar conta.");
            var account = new Infrastructure.Account();
            bool result = _accountRepository.Delete(acc);
            return result;
        }
    }
}
