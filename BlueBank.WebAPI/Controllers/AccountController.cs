using System;
using Domain.Core.DTOs;
using Domain.Core.Interfaces;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BlueBank.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        public ObjectResult CreateAccount([FromBody] CreateAccountDto dto)
        {
            try
            {
                var request = new CreateAccountRequest(dto, _accountRepository);
                var result = request.Create();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Mensagem de erro:" + e.Message);
            }
        }

        //[HttpPut]
        //public ObjectResult UpdateAccount([FromBody] AccountDto dto)
        //{
        //    try
        //    {
        //        var request = new UpdateAccountRequest(dto);
        //        var result = request.Update();
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest("Mensagem de erro:" + e.Message);
        //    }
        //}

        [HttpGet("{accountNumber}")]
        public ObjectResult GetAccount(int accountNumber)
        {
            var request = new GetAccountRequest(accountNumber, _accountRepository);

            try
            {
                var result = request.Get();
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
            var request = new DeleteAccountRequest(accountNumber, _accountRepository);

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
    }
}