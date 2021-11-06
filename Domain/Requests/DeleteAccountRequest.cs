using Domain.Core.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    public class DeleteAccountRequest
    {
        //    private readonly int _accountNumber;
        //    private readonly AccountRepository _accountRepository;

        //    public DeleteAccountRequest(int accountNumber)
        //    {
        //        _accountNumber = accountNumber;
        //        _accountRepository = new AccountRepository();
        //    }

        //    public bool Validate()
        //    {
        //        Account acc = _accountRepository.Read(_dto.AccountNumber);
        //        if (acc != null) return false;
        //        return true;
        //    }

        //    public bool Delete()
        //    {
        //        if (!Validate()) throw new Exception("Erro ao deletar conta.");

        //        var account = new Infrastructure.Account();

        //        // var findAccount = _context.Accounts.SingleOrDefault(x => x.Id == account);

        //        bool result = _accountRepository.Delete(account);
        //        // int response = new();

        //        return result;
        //    }
    }
}
