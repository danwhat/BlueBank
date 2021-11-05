using Domain.Core.DTOs;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlueBank.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
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
                return BadRequest("Mensagem de errooooooooo:" + e.Message);
            }
        }

        [HttpDelete("{doc}")]
        public ObjectResult DeleteContact(string doc, [FromBody] ContactDto contactDto)
        {
            var request = new DeleteContactRequest(doc, contactDto.PhoneNumber);
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
