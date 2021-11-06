using Domain.Core.DTOs;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlueBank.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepositController : ControllerBase
    {
        [HttpPost("{accountNumber}")]
        public ObjectResult Deposit(int accountNumber, [FromBody] TransactionDTO transation)
        {
            try
            {
                var request = new DepositRequest(accountNumber, transation);
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
