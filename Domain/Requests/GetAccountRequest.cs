using System;
using Domain.Core.DTOs;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces;
using Domain.Entities;

namespace Domain.Requests
{
    public class GetAccountRequest
    {
        private readonly int _accountNumber;
        private readonly IAccountRepository _accountRepository;

        public GetAccountRequest(int accountNumber, IAccountRepository accountRepository)
        {
            _accountNumber = accountNumber;
            _accountRepository = accountRepository;
        }

        public AccountDto Get()
        {
            try
            {
                Account result = _accountRepository.Get(_accountNumber);
                AccountDto response = new(result);
                return response;
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