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
        [HttpPost("{accountNumber}")]
        public ObjectResult Withdraw(int accountNumber, [FromBody] TransactionDTO transation)
        {
            try
            {
                var request = new WithdrawRequest(accountNumber, transation);
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
