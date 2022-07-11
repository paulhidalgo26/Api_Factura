using ApiFactura.Models;
using ApiFactura.Models.Exceptions;
using ApiFactura.Models.Request;
using ApiFactura.Models.Response;
using ApiFactura.Services;
using ApiFactura.Services.ProductServices;
using Microsoft.Data.SqlClient;

namespace ApiFactura.Services.SaleServices
{
    public class SaleService: ISaleService
    {
        public Response Add(SaleRequest saleRequested)
        {
            string cadena = "Data Source=SQL8002.site4now.net;Initial Catalog=db_a88d98_appmivil;User Id=db_a88d98_appmivil_admin;Password=p.hidalgo12;Encrypt=False";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            SqlCommand command = new SqlCommand("select SYSDATETIME()", conexion);
            string fecha = command.ExecuteScalar().ToString();
            conexion.Close();
            Response R = new Response();
            ProductService pS = new ProductService();
            using (Context DB = new Context())
            {      
                using (var transaction = DB.Database.BeginTransaction())
                {
                    try
                    {
                        decimal total = 0;
                        foreach (var saleDetail in saleRequested.saleDetails)
                        {
                            total += DB.Products.Find(saleDetail.IDProduct).UnitPrice * saleDetail.Quantity;
                        }
                        var newSale = new Sale();
                        newSale.Total = total;// * (decimal)1.1
                        newSale.Date = DateTime.Parse(fecha);//consultar base el servidor 
                        newSale.IduserClient = saleRequested.IDUserClient;
                        DB.Sales.Add(newSale);
                        DB.SaveChanges();
                        foreach (var saleDetail in saleRequested.saleDetails)
                        {
                                var newSaleDetail = new Models.SaleDetail();
                                newSaleDetail.Idsale = newSale.Id;
                                newSaleDetail.Idproduct = saleDetail.IDProduct;
                                newSaleDetail.Quantity = saleDetail.Quantity;
                                pS.ReduceQuantityBySale(saleDetail.IDProduct, saleDetail.Quantity);
                                DB.SaleDetails.Add(newSaleDetail);
                                DB.SaveChanges();
                        }
                        transaction.Commit();
                        R.Success = true;
                        R.Message = "Succesful sale transaction";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        R.Message = ex.Message;
                    }
                    return R;
                }
            }
        }
    }
}
