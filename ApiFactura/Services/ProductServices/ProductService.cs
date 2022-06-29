using ApiFactura.Models;
using ApiFactura.Models.Exceptions;
using ApiFactura.Models.Request;
using ApiFactura.Models.Response;

namespace ApiFactura.Services.ProductServices
{
    public class ProductService: IProductService
    {
        public void ReduceQuantityBySale(int product, int qty)
        {
            using (Context DB = new Context())
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    try
                    {
                        Product editProduct = DB.Products.Find(product);
                        editProduct.Quantity -= qty;
                       DB.Entry(editProduct).State = Microsoft.EntityFrameworkCore
                            .EntityState.Modified;
                        DB.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }      
        }
        public Response CheckAvailableProductStock(SaleRequest list)
        {
            Response R = new Response();
            try
            {
                using (Context DB = new Context())
                {
                    foreach (var saleProduct in list.saleDetails)
                    {
                        Product checkProduct = DB.Products.Find(saleProduct.IDProduct);
                        if (checkProduct.Quantity < saleProduct.Quantity)
                        {
                            throw new OutOfStock(checkProduct.Id);
                        }
                    }
                }
                R.Success = true;
            }
            catch(OutOfStock EX)
            {
                R.Message = EX.Message;
            }
            catch (Exception ex)
            {
                R.Message = ex.Message;
            }
            return R;
        }
        public Response AddProduct(ProductRequest request)
        {
            Response R = new Response();
            using(Context DB = new Context())
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    try
                    {
                        var newProduct = new Product();
                        newProduct.Name = request.Name;
                        newProduct.Genre = request.Genre;
                        newProduct.Quantity = request.Quantity;
                        newProduct.UnitPrice = request.UnitPrice;
                        newProduct.Cost = request.Cost;
                        //newProduct.ImageURL = request.ImageURL;

                        DB.Products.Add(newProduct);
                        DB.SaveChanges();
                        R.Success = true;
                        R.Message = "New Product added succesful";
                        transaction.Commit();
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
        public Response UpdateProduct(ProductRequest request, int id)
        {
            Response R = new Response();
            using (Context DB = new Context())
            {
                    try
                    {
                        var newProduct = new Product();
                        newProduct.Id = id;
                        newProduct.Name = request.Name;
                        newProduct.Genre = request.Genre;
                        newProduct.Quantity = request.Quantity;
                        newProduct.UnitPrice = request.UnitPrice;
                        newProduct.Cost = request.Cost;
                        //newProduct.ImageURL = request.ImageURL;

                        DB.Products.Update(newProduct);
                        DB.SaveChanges();
                        R.Success = true;
                        R.Message = "Product updated succesful";
                    }
                    catch (Exception ex)
                    {
                        R.Message = ex.Message;
                    }
                    return R;
            }           
        }       
        public Response DeleteProduct(int id)
        {
            Response R = new Response();
            using (Context DB = new Context())
            {
                try
                {
                    var newProduct = new Product();
                    newProduct = DB.Products.Find(id);
                    DB.Products.Remove(newProduct);
                    DB.SaveChanges();
                    R.Success = true;
                    R.Message = "Product " + id + " deleted succesfully";
                }
                catch (Exception ex)
                {
                    R.Message = ex.Message;
                }
                return R;
            }
        }
    }   
}