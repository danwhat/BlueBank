using Domain.Core.DTOs;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces;
using Domain.Entities;
using Domain.Services.Validations;
using System;

namespace Domain.Requests
{
    public class WithdrawRequest
    {
        public WithdrawRequest(
            int AccountNumber,
            TransactionDTO dto,
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _accountNumber = AccountNumber;
            _dto = dto;
            _accountRepository = accountRepository;
            _transactionRepositoy = transactionRepository;
        }

        private readonly int _accountNumber;
        private readonly TransactionDTO _dto;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepositoy;

        public void Validation()
        {
            Validations.ThisAccountExistsValidation(_accountRepository, _accountNumber);
            Validations.SufficientBalanceValidation(_accountRepository, _accountNumber, _dto.Value);
        }

        public TransactionResponseDTO Withdraw()
        {
            Validation();

            Account acc = _accountRepository.Get(_accountNumber);

            Transaction transaction = new();
            transaction.AccountFrom = acc;
            transaction.Value = _dto.Value;

            try
            {
                Transaction transactionDB = _transactionRepositoy.Create(transaction);
                Account accAtualizada = _accountRepository.Get(_accountNumber);
                var transactionResponse = new TransactionResponseDTO
                {
                    Message = "Saque realizado com sucesso.",
                    OldBalance = acc.Balance,
                    CurrentBalance = accAtualizada.Balance
                };
                return transactionResponse;
            }
            catch (ServerException e)
            {
                throw new ServerException("Deu erro aqui."+ e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Deu erro aqui." + e.Message);
            }
        }
    }
}
