using ApiFactura.Models.Request;
using ApiFactura.Models.Response;
using ApiFactura.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFactura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserClientController : ControllerBase
    {
        private IUserClientService _userClientService;
        public UserClientController(IUserClientService userClientService)
        {
            _userClientService = userClientService;
        }

        [HttpPost("login")]
        public IActionResult Authentification([FromBody]AuthRequest model)
        {
            Response R = new Response();
            var userResponse = _userClientService.Auth(model);

            if (userResponse == null)
            {
                R.Message = "Invalid Email or password";
                return BadRequest(R);
            }
            R.Success = true;
            R.Message = "Succesful login";
            R.Data = userResponse;

            return Ok(R);
        }

    }
}
