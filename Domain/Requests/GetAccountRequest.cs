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

        public Account Get()
        {
            try
            {
                Account result = _accountRepository.Get(_accountNumber);
                return result;
            }
            catch (ServerException)
            {
                throw new InvalidInputException("Conta não encontrada.");
            }
        }
    }
}
