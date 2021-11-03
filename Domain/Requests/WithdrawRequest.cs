using Domain.Core.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using System;

namespace Domain.Requests
{
    public class WithdrawRequest
    {
        public WithdrawRequest(int AccountNumber, TransactionDTO dto)
        {
            _accountNumber = AccountNumber;
            _dto = dto;
            _accountRepository = new AccountRepository();
            _transactionRepositoy = new TransactionRepository();
        }

        private readonly int _accountNumber;
        private readonly TransactionDTO _dto;
        private readonly AccountRepository _accountRepository;
        private readonly TransactionRepository _transactionRepositoy;

        public bool Validate()
        {
            // valida se a conta existe
            Account acc = _accountRepository.Get(_accountNumber);
            if (acc != null) return false;
            // Checar se tem saldo
            var accdb = _accountRepository.Get(_accountNumber);
            if (_dto.Value > accdb.Balance) return false;
            // validado
            return true;
        }
        
        public AccountDto Withdraw()
        {
            if (!Validate()) throw new Exception("Faltaou tal coisa aqui");

            Account acc = _accountRepository.Get(_accountNumber);

            Transaction transaction = new();
            transaction.AccountFrom = acc;
            transaction.Value = _dto.Value;

            Transaction transactionDB = _transactionRepositoy.Create(transaction);

        }

    }
}
