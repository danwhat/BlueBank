using System;
using Domain.Core.DTOs;
using Domain.Core.Interfaces;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BlueBank.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepositController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public DepositController(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpPost("{accountNumber}")]
        public ObjectResult Deposit(int accountNumber, [FromBody] TransactionValueDto transation)
        {
            try
            {
                var request = new DepositRequest(accountNumber, transation, _accountRepository, _transactionRepository);
                var response = request.Deposit();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest("Mensagem de erro:" + e.Message);
            }
        }
    }
}