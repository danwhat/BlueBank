using System;
using Domain.Core.DTOs;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces;
using Domain.Entities;
using Domain.Services.Validations;

namespace Domain.Requests
{
    public class DepositRequest
    {
        private readonly int _accountNumber;

        private readonly TransactionValueDto _dto;

        private readonly IAccountRepository _accountRepository;

        private readonly ITransactionRepository _transactionRepository;

        public DepositRequest(
            int AccountNumber,
            TransactionValueDto dto,
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _accountNumber = AccountNumber;
            _dto = dto;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public void Validation()
        {
            Validations.ThisAccountExistsValidation(_accountRepository, _accountNumber);
        }

        public TransactionResponseDto Deposit()
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
                var transactionResponse = new TransactionResponseDto
                {
                    Message = "Depósito realizado com sucesso.",
                    OldBalance = acc.Balance,
                    CurrentBalance = accAtualizada.Balance
                };
                return transactionResponse;
            }
            catch (ServerException e)
            {
                throw new Exception("Ocorreu um erro: " + e.Message);
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro interno.");
            }
        }
    }
}