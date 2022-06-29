using System;
using System.Collections.Generic;

namespace ApiFactura.Models
{
    public partial class Product
    {
        public Product()
        {
            SaleDetails = new HashSet<SaleDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Cost { get; set; }
        //public string ImageURL { get; set; }

        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
