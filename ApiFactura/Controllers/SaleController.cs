using ApiFactura.Models;
using ApiFactura.Models.Exceptions;
using ApiFactura.Models.Request;
using ApiFactura.Models.Response;
using ApiFactura.Services;
using ApiFactura.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiFactura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        #region Instance
        private ISaleService _sale;
        public SaleController(ISaleService sale)
        {
            this._sale = sale;
        }
        #endregion

        #region GetAllSales
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Get()
        {
            Response R = new Response();
            try
            {
                using (Context DB = new Context())
                {
                    var saleList = DB.Sales.OrderBy(b => b.Date)
                        .ToList();
                    R.Success = true;
                    R.Message = "SaleGet Succesful";
                    R.Data = saleList;
                }
            }
            catch (Exception ex)
            {
                R.Message = ex.Message;
            }
            return Ok(R);
        }
        #endregion

        #region GetSalesByUserID

        [HttpGet("{user:int}")]
        [Authorize(Roles = "admin, client")]
        public IActionResult Get(int user)
        {
            Response R = new Response();
            try
            {
                using (Context DB = new Context())
                {
                    var saleList = DB.Sales.Where(sale => sale.IduserClient == user).OrderByDescending(b=>b.Date).ToList();
                    R.Success = true;
                    R.Message = "SaleSpecified Get Succesful";
                    R.Data = saleList;
                }
            }
            catch (Exception ex)
            {
                R.Message = ex.Message;
            }
            return Ok(R);
        }
        #endregion

        #region AddSale
        [Authorize(Roles = "client")]
        [HttpPost]
        public IActionResult Add(SaleRequest saleRequested)
        {
            Response R = new Response();
            ProductService pS = new ProductService();
            try
            {
                R = pS.CheckAvailableProductStock(saleRequested);
                if (R.Success)
                {
                    R = this._sale.Add(saleRequested);
                }
            }
            catch (Exception ex)
            {
                R.Message = ex.Message;  
            }
            return Ok(R);
        }
        #endregion
    }
}