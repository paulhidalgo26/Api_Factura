using System;
using System.Collections.Generic;

namespace ApiFactura.Models
{
    public partial class SaleDetail
    {
        public long Id { get; set; }
        public long Idsale { get; set; }
        public int Idproduct { get; set; }
        public int Quantity { get; set; }

        public virtual Product IdproductNavigation { get; set; } = null!;
        public virtual Sale IdsaleNavigation { get; set; } = null!;
    }
}