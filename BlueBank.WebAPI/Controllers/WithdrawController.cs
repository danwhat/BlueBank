using Domain.Core.DTOs;
using Domain.Core.Interfaces;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlueBank.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WithdrawController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public WithdrawController(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpPost("{accountNumber}")]
        public ObjectResult Withdraw(int accountNumber, [FromBody] TransactionDTO transation)
        {
            try
            {
                var request = new WithdrawRequest(accountNumber, transation, _accountRepository, _transactionRepository);
                var response = request.Withdraw();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest("Mensagem de erro:" + e.Message);
            }
        }
    }
}
