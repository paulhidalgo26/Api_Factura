using ApiFactura.Models;
using ApiFactura.Models.Exceptions;
using ApiFactura.Models.Request;
using ApiFactura.Models.Response;
using ApiFactura.Services;
using ApiFactura.Services.ProductServices;

namespace ApiFactura.Services.SaleServices
{
    public class SaleService: ISaleService
    {
        public Response Add(SaleRequest saleRequested)
        {
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
                        newSale.Total = total * (decimal)1.1;
                        newSale.Date = DateTime.Now;
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
