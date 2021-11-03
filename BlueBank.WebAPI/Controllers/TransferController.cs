using Domain.Core.DTOs;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlueBank.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : ControllerBase
    {
        [HttpPost("{accountNumber}")]
        public ObjectResult Transfer(int accountNumber, [FromBody] TransactionDTO transation)
        {
            try
            {
                var request = new TransferRequest(accountNumber, transation);
                var response = request.Transfer();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest("Mensagem de erro:" + e.Message);
            }
        }
    }
}
