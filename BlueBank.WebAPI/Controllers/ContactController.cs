using Domain.Core.DTOs;
using Domain.Core.Interfaces;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlueBank.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAccountRepository _accountRepository;

        public ContactController(IPersonRepository personRepository, IAccountRepository accountRepository)
        {
            _personRepository = personRepository;
            _accountRepository = accountRepository;
        }

        [HttpPost("{accountNumber}")]
        public ObjectResult CreateContact(int accountNumber, [FromBody] PersonRequestDto phone)
        {
            try
            {
                var request = new CreateContactRequest(accountNumber, phone, _personRepository, _accountRepository);
                var result = request.Create();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Mensagem de erro:" + e.Message);
            }
        }

        [HttpDelete("{document}")]
        public ObjectResult DeleteContact(string document, [FromBody] ContactDto contactDto)
        {
            var request = new DeleteContactRequest(document, contactDto.PhoneNumber, _personRepository);
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
