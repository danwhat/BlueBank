using System;
using Domain.Core.DTOs;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces;
using Domain.Entities;
using Domain.Services.Validations;

namespace Domain.Requests
{
    public class TransferRequest
    {
        private readonly int _accountNumber;

        private readonly TransactionDto _dto;

        private readonly IAccountRepository _accountRepository;

        private readonly ITransactionRepository _transactionRepository;

        public TransferRequest(
            int AccountNumber,
            TransactionDto dto,
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
            Validations.ThisAccountExistsValidation(_accountRepository, _dto.AccountNumberTo);
            Validations.SufficientBalanceValidation(_accountRepository, _accountNumber, _dto.Value);
        }

        public TransactionResponseDto Transfer()
        {
            Validation();

            Account accFrom = _accountRepository.Get(_accountNumber);
            Account accTo = _accountRepository.Get(_dto.AccountNumberTo);

            Transaction transaction = new();
            transaction.AccountFrom = accFrom;
            transaction.AccountTo = accTo;
            transaction.Value = _dto.Value;

            try
            {
                Transaction transactionDB = _transactionRepository.Create(transaction);
                Account accAtualizada = _accountRepository.Get(_accountNumber);
                var transactionResponse = new TransactionResponseDto
                {
                    Message = $"Transferência para {accTo.Person.Name} realizada com sucesso.",
                    OldBalance = accFrom.Balance,
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