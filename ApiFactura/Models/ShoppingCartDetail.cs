using System;
using System.Collections.Generic;

namespace ApiFactura.Models
{
    public partial class ShoppingCartDetail
    {
        public int Id { get; set; }
        public int? IdshoppingCart { get; set; }
        public int? Idproduct { get; set; }
        public int? Quantity { get; set; }

        public virtual ShoppingCart? IdshoppingCartNavigation { get; set; }
    }
}
