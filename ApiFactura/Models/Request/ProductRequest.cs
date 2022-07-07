namespace ApiFactura.Models.Request
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Cost { get; set; }
       // public string ImageURL { get; set; }

    }
}
