using Domain.Core.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using System;

namespace Domain.Requests
{
    public class TransferRequest
    {
        public TransferRequest(int AccountNumber, TransactionDTO dto)
        {
            _accountNumber = AccountNumber;
            _dto = dto;
            //_accountRepository = new AccountRepository();
            //_transactionRepositoy = new TransactionRepository();
        }

        private readonly int _accountNumber;
        private readonly TransactionDTO _dto;
        private readonly AccountRepository _accountRepository;
        private readonly TransactionRepository _transactionRepositoy;

        public bool Validate()
        {
            Account accFrom = _accountRepository.Get(_accountNumber);
            Account accTo = _accountRepository.Get(_dto.AccountNumberTo);
            // valida se a conta origem existe
            if (accFrom == null) return false;
            // valida se a conta destino existe
            if (accTo == null) return false;
            // Checar se tem saldo
            if (_dto.Value > accFrom.Balance) return false;
            // validado
            return true;
        }
        
        public TransactionResponseDTO Transfer()
        {
            if (!Validate()) throw new Exception("Não passou na validação");

            Account accFrom = _accountRepository.Get(_accountNumber);
            Account accTo = _accountRepository.Get(_dto.AccountNumberTo);

            Transaction transaction = new();
            transaction.AccountFrom = accFrom;
            transaction.AccountTo = accTo;
            transaction.Value = _dto.Value;

            try
            {
                Transaction transactionDB = _transactionRepositoy.Create(transaction);
                Account accAtualizada = _accountRepository.Get(_accountNumber);
                var transactionResponse = new TransactionResponseDTO();
                transactionResponse.Message = $"Transferencia para {accTo.Person.Name} realizado com sucesso.";
                transactionResponse.OldBalance = accFrom.Balance;
                transactionResponse.CurrentBalance = accAtualizada.Balance;
                return transactionResponse;
            }
            catch(Exception e)
            {
                throw new Exception($"Falha de comunicação com o Repository: {e.Message}");
            }
        }
    }
}
