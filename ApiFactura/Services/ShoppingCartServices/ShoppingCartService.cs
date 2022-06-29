using ApiFactura.Models;
using ApiFactura.Models.Request;
using ApiFactura.Models.Response;

namespace ApiFactura.Services.ShoppingCartServices
{
    public class ShoppingCartService: IShoppingCartService
    {
        public Response Get()  
        {
            Response R = new Response();
            try
            {
                using (carShopinContext DB = new carShopinContext())
                {
                    var shoppingCartsList = DB.ShoppingCarts
                        .ToList();
                    R.Success = true;
                    R.Message = "ShoppingCar GET Succesful";
                    R.Data = shoppingCartsList;
                }
            }
            catch (Exception ex)
            {
                R.Message = ex.Message;
            }
            return R;
        }
        public Response Add(ShoppingCartRequest request)
        {
            Response R = new Response();

            using (carShopinContext DB = new carShopinContext())
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    try
                    {
                        var newCart = new ShoppingCart();
                        newCart.IduserClient = request.IDUserClient;
                        DeleteCartByUserID(request.IDUserClient);
                        DB.ShoppingCarts.Add(newCart);
                        DB.SaveChanges();
                        foreach (var cartDetail in request.ShopCartDetails)
                        {
                            var newCartDetail = new Models.ShoppingCartDetail();
                            newCartDetail.IdshoppingCart = newCart.Id;
                            newCartDetail.Idproduct = cartDetail.IDProduct;
                            newCartDetail.Quantity = cartDetail.Quantity;
                            DB.ShoppingCartDetails.Add(newCartDetail);
                            DB.SaveChanges();
                        }
                        transaction.Commit();
                        R.Success = true;
                        R.Message = "Succesful ShopCart transaction";

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
        public Response DeleteCartByUserID(int user)
        {
            Response R = new Response();
            R.Message = "Cart doesn't exists for user: " + user;
            using (carShopinContext DB = new carShopinContext())
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    try
                    {
                        var newCart = DB.ShoppingCarts.SingleOrDefault(cart => cart.IduserClient == user);
                        if (newCart != null)
                        {
                            List<Models.ShoppingCartDetail> details
                                = DB.ShoppingCartDetails.Where(details => details.IdshoppingCart == newCart.Id).ToList();
                            foreach (var item in details)
                            {
                                DB.ShoppingCartDetails.Remove(item);
                                DB.SaveChanges();
                            }
                            DB.ShoppingCarts.Remove(newCart);
                            DB.SaveChanges();
                            transaction.Commit();
                            R.Success = true;
                            R.Message = "Cart from user: " + user + " deleted";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return R;
        }
        public Response GetCartByIDUserClient(int user)
        {
            Response R = new Response();
            try
            {
                using (carShopinContext DB = new carShopinContext())
                {
                    var cart = DB.ShoppingCarts.Where(cart => cart.IduserClient == user).ToList();
                    R.Data = GetDetailsByIDCart(cart[0].Id);
                    R.Success = true;
                }
            }
            catch (Exception ex)
            {
                R.Message = ex.Message;
            }
            return R;
        }
        public List<Models.ShoppingCartDetail> GetDetailsByIDCart(int id)
        {
            List<Models.ShoppingCartDetail> R = new List<Models.ShoppingCartDetail>();
            try
            {
                using (carShopinContext DB = new carShopinContext())
                {
                    var cart = DB.ShoppingCartDetails.Where(details => details.IdshoppingCart == id).ToList();
                    R = cart;
                }
            }
            catch (Exception ex)
            {
            }
            return R;
        }
    }
}