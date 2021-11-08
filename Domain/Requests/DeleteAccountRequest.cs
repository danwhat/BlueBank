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
            Account acc = _accountRepository.Get(_accountNumber);
            bool result = _accountRepository.Delete(acc);
            return result;
        }
    }
}
