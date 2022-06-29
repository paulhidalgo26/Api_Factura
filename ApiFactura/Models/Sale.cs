using System;
using System.Collections.Generic;

namespace ApiFactura.Models
{
    public partial class Sale
    {
        public Sale()
        {
            SaleDetails = new HashSet<SaleDetail>();
        }

        public long Id { get; set; }
        public DateTime Date { get; set; }
        public int IduserClient { get; set; }
        public decimal Total { get; set; }

        public virtual UserClient IduserClientNavigation { get; set; } = null!;
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
