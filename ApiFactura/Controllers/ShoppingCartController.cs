using ApiFactura.Models.Request;
using ApiFactura.Models.Response;
using ApiFactura.Services.ShoppingCartServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFactura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "client, admin")]
    public class ShoppingCartController : ControllerBase
    {
        #region Instance
        private IShoppingCartService _cart;
        public ShoppingCartController(IShoppingCartService cart)
        {
            this._cart = cart;
        }
        #endregion

        #region GetCartByUserID
        [HttpGet("{id:int}")]
        [Authorize(Roles = "client")]
        public IActionResult Get(int id)
        {
            Response R = new Response();
            R = this._cart.GetCartByIDUserClient(id);
            return Ok(R);
        }
        #endregion

        #region AddUpdateCart
        [HttpPost]
        [Authorize(Roles = "client")]
        public IActionResult Add(ShoppingCartRequest request)
        {
            Response R = new Response();
            try
            {
                R = this._cart.Add(request);
            } catch(Exception ex)
            {
                R.Message = ex.Message;
            }
            return Ok(R);
        }
        #endregion

        #region DeleteCart
        [HttpDelete("{user:int}")]
        [Authorize(Roles = "client")]
        public IActionResult Delete(int user)
        {
            Response R = new Response();
            R = this._cart.DeleteCartByUserID(user);
            return Ok(R);
        }
        #endregion
    }
}