using System;
using Domain.Core.DTOs;
using Domain.Core.Interfaces;
using Domain.Entities;
using Domain.Services.Validations;

namespace Domain.Requests
{
    public class DepositRequest
    {
        public DepositRequest(
            int AccountNumber,
            TransactionDTO dto,
            IAccountRepository accountRepository, 
            ITransactionRepository transactionRepository)
        {
            _accountNumber = AccountNumber;
            _dto = dto;                        
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        private readonly int _accountNumber;
        private readonly TransactionDTO _dto;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

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
                Transaction transactionDB = _transactionRepository.Create(transaction);
                Account accAtualizada = _accountRepository.Get(_accountNumber);
                var transactionResponse = new TransactionResponseDTO
                {
                    Message = "Deposito realizado com sucesso.",
                    OldBalance = acc.Balance,
                    CurrentBalance = accAtualizada.Balance
                };
                return transactionResponse;
            }
            catch(Exception e)
            {
                throw new Exception("Deu erro aqui." + e.Message);
            }
        }
    }
}
