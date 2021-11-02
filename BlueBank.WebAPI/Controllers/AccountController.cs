using Domain.Core.DTOs;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlueBank.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public int Get([FromRoute] int accountNumber)
        {
            return accountNumber;
        }

        [HttpPost]
        public ObjectResult CreateAccount([FromBody] NewAccountDto dto)
        {
            //var dto = new NewAccountDto() { Doc = "099999", Name = "Teste" };
            var request = new CreateAccountRequest(dto);
            //if (request.Validate() == false)
            //{
            //    BadRequest();
            //}
            try
            {
                var result = request.Create();
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest("Mensagem de erro:" + e.Message);
            }
            
        }
    }
}
