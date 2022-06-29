using ApiFactura.Models;
using ApiFactura.Models.Request;
using ApiFactura.Models.Response;
using ApiFactura.Services.ClientsServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFactura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        #region Instace
        private IClientsService _client;
        public ClientsController(IClientsService client)
        {
            this._client = client;
        }
        #endregion

        #region GetAllClients
        [HttpGet]
        [Authorize(Roles = "admin, client")]
        public IActionResult Get()
        {
            Response R = new Response();
            try
            {
                using (Context DB = new Context())
                {
                    var productList = DB.UserClients.OrderBy(b => b.Id)
                           .ToList();
                    R = ClientsService.EmptyPasswords(productList);
                    R.Success = true;
                    R.Message = "ClientsGetSucessful";
                }
            }
            catch (Exception ex)
            {
                R.Message = ex.Message;
            }
            return Ok(R);
        }
        #endregion

        #region GetClientByID
        [HttpGet("{id:int}")]
        [Authorize(Roles = "admin, client")]
        public IActionResult GetSpecifiedClient(int id)
        {
            Response R = new Response();
            try
            {
                using (Context DB = new Context())
                {
                    var client = DB.UserClients.Where(cliebt => cliebt.Id == id).ToList();
                    R = ClientsService.EmptyPasswords(client);
                    R.Success = true;
                    R.Message = "GetSpecifiedClient Succesful";
                }
            }
            catch (Exception ex)
            {
                R.Message = ex.Message;
            }
            return Ok(R);
        }
        #endregion

        #region AddNewClient
        [HttpPost]
        public IActionResult Add(NewClientRequest client)
        {
            Response R = new Response();
            try
            {   
                R = this._client.Add(client);    
            }
            catch (Exception ex)
            {
                R.Message = ex.Message;
            }
            return Ok(R);
        }
        #endregion

        #region UpdateClient
        [HttpPost("{id:int}")]
        [Authorize(Roles = "admin, client")]
        public IActionResult UpdateClient(UpdateUserRequest request, int id)
        {
            Response R = new Response();
            R = this._client.UpdateClient(request, id);
            return Ok(R);
        }
        #endregion

        #region DeleteClient
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteClient(int id)
        {
            Response R = new Response();
            R = this._client.DeleteClient(id);
            return Ok(R);
        }
        #endregion
    }
}
