using Domain.Core.DTOs;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlueBank.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WithdrawController : ControllerBase
    {
        [HttpGet("{accountNumber}")]
        public ObjectResult Withdraw(string accountNumber, [FromBody] TransactionDTO transation)
        {
            return Ok($"Conta: {accountNumber}, Valor sacado: {transation.Value}");
        }
    }
}
