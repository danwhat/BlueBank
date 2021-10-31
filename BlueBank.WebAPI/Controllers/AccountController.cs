using Domain.Core.DTOs;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;

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
        public OkObjectResult CreateAccount([FromBody] NewAccountDto dto)
        {
            //var dto = new NewAccountDto() { Doc = "099999", Name = "Teste" };
            var request = new CreateAccountRequest(dto);
            if (request.Validate() == false)
            {
                BadRequest();
            }

            var result = request.Create();

            return Ok(result);
        }
    }
}
