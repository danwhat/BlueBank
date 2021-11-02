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
        public string Get()
        {
            return "Olá mundo!";
        }

        [HttpPost]
        public ObjectResult CreateAccount([FromBody] AccountDto dto)
        {
            try
            {
                var request = new CreateAccountRequest(dto);
                var result = request.Create();
                return Ok(result);
            }
            catch(Exception e)
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
    }
}
