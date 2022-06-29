using System.ComponentModel.DataAnnotations;

namespace ApiFactura.Models.Request
{
    public class ShoppingCartRequest
    {
        [Required]
        public int IDUserClient { get; set; }
        [Required]
        public List<ShoppingCartDetail>? ShopCartDetails { get; set; }
    }
    public class ShoppingCartDetail
    {
        [Required]
        public int IDProduct { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
