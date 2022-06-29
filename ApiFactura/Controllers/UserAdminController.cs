using ApiFactura.Models.Request;
using ApiFactura.Models.Response;
using ApiFactura.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFactura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAdminController : ControllerBase
    {
        #region Instance
        private IUserAdminService _userAdminService;
        public UserAdminController(IUserAdminService userAdminService)
        {
            _userAdminService = userAdminService;
        }
        #endregion

        #region LoginAdmin
        [HttpPost("login")]
        public IActionResult Authentification([FromBody] AuthRequest model)
        {
            Response R = new Response();
            var userResponse = _userAdminService.Auth(model);

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
        #endregion
    }
}