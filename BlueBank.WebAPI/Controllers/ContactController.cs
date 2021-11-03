using Domain.Core.DTOs;
using Domain.Requests;
using Domain.Services.Requests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlueBank.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        [HttpDelete("{doc}")]
        public ObjectResult DeleteAccount(string doc, [FromBody] string phoneNumber)
        {
            var request = new DeleteContactRequest(doc, phoneNumber);

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
