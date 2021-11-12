using System;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces;
using Domain.Entities;
using Domain.Services.Validations;

namespace Domain.Requests
{
    public class DeleteAccountRequest
    {
        private readonly int _accountNumber;
        private readonly IAccountRepository _accountRepository;

        public DeleteAccountRequest(int accountNumber, IAccountRepository accountRepository)
        {
            _accountNumber = accountNumber;
            _accountRepository = accountRepository;
        }

        public void Validation()
        {
            Validations.ThisAccountExistsValidation(_accountRepository, _accountNumber);
        }

        public bool Delete()
        {
            Validation();
            try
            {
                Account acc = _accountRepository.Get(_accountNumber);
                bool result = _accountRepository.Delete(acc);
                return result;
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