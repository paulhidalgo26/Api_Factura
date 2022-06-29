namespace ApiFactura.Models.Exceptions
{
    public class OutOfStock: Exception
    {
        public OutOfStock()
        {

        }
        public OutOfStock(int product): 
            base(String.Format("Product {0} out of stock", product))
        { }
    }
}
