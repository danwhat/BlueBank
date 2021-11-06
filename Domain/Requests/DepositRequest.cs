using Domain.Core.DTOs;
using Domain.Entities;
using Domain.Services.Validations;
using Infrastructure.Repositories;
using System;

namespace Domain.Requests
{
    public class DepositRequest
    {
        public DepositRequest(int AccountNumber, TransactionDTO dto)
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

        public void Validation()
        {
            Validations.ThisAccountExistsValidation(_accountRepository, _accountNumber);
        }
        
        public TransactionResponseDTO Deposit()
        {
            Validation();

            Account acc = _accountRepository.Get(_accountNumber);

            Transaction transaction = new();
            transaction.AccountTo = acc;
            transaction.Value = _dto.Value;

            try
            {
                Transaction transactionDB = _transactionRepositoy.Create(transaction);
                Account accAtualizada = _accountRepository.Get(_accountNumber);
                var transactionResponse = new TransactionResponseDTO();
                transactionResponse.Message = "Deposito realizado com sucesso.";
                transactionResponse.OldBalance = acc.Balance;
                transactionResponse.CurrentBalance = accAtualizada.Balance;
                return transactionResponse;
            }
            catch(Exception e)
            {
                throw new Exception("Deu erro aqui." + e.Message);
            }
        }
    }
}
