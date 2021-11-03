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
    public class GetAccountRequest
    {
        private readonly int _accountNumber;
        private readonly AccountRepository _accountRepository;

        public GetAccountRequest(int accountNumber)
        {
            _accountNumber = accountNumber;
            _accountRepository = new AccountRepository();
        }

        public Account Get()
        {
            //var account = new Infrastructure.Account;

            // var findAccount = _context.Accounts.SingleOrDefault(x => x.Id == account);

            Account result = _accountRepository.Get(_accountNumber);
            // int response = new();

            return result;
        }
    }
}
