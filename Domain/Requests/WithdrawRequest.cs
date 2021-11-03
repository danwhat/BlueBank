using Domain.Core.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using System;

namespace Domain.Requests
{
    public class WithdrawRequest
    {
        public WithdrawRequest(TransactionDTO dto)
        {
            _dto = dto;
            _accountRepository = new AccountRepository();
        }

        private readonly TransactionDTO _dto;
        private readonly BalanceRepository _accountRepository;

        public bool Validate()
        {
            // Checar se conta existe
            // Checar se tem saldo
            return true;
        }
        
        public AccountDto Withdraw()
        {
            if (!Validate()) throw new Exception("Faltaou tal coisa aqui");

            

            Account newAccount = _accountRepository.Create(account);
            AccountDto response = new(newAccount);
            return response;
        }

    }
}
