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
        [HttpPost]
        public ObjectResult CreateAccount([FromBody] AccountDto dto)
        {
            try
            {
                var request = new CreateAccountRequest(dto);
                var result = request.Create();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Mensagem de erro:" + e.Message);
            }

        }

        [HttpPut]
        public ObjectResult UpdateAccount([FromBody] AccountDto dto)
        {
            try
            {
                var request = new UpdateAccountRequest(dto);
                var result = request.Update();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Mensagem de erro:" + e.Message);
            }
        }

        [HttpDelete("{accountNumber}")]
        public ObjectResult DeleteAccount(int accountNumber)
        {
            var request = new DeleteAccountRequest(accountNumber);

            try
            {
                var result = request.Delete();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Mensagem de erro:" + e.Message);
            }
        }

        [HttpPost("{accountNumber}")]
        public ObjectResult CreateContact(int accountNumber, [FromBody] PersonRequestDto phone)
        {
            try
            {
                var request = new CreateContactRequest(accountNumber, phone);
                var result = request.Create();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Mensagem de erro:" + e.Message);
            }
        }
    }    
}
