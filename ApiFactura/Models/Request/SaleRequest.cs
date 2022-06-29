using System.ComponentModel.DataAnnotations;

namespace ApiFactura.Models.Request
{
    /*Here, I define how(format) the FrontEnd must send to the BackEnd
     the data to receive a Sale Data Type*/
    public class SaleRequest
    {
        [Required]
        [Range(1, Double.MaxValue, ErrorMessage =
            "IDClient invalid")]
        [ClientExists(ErrorMessage =
            "IDClient doesn't exists")]
        public int IDUserClient { get; set; }   
        [Required]
        [MinLength(1, ErrorMessage =
            "Sale without products")]
        public List<SaleDetail> saleDetails { get; set; }
    }

    public class SaleDetail
    {
        [Required]
        [ProductExists(ErrorMessage =
            "The product doesn't exists in DB")]
        public int IDProduct { get; set; }
        [Required]
        [Range(1, Double.MaxValue, ErrorMessage =
            "Quantity must be superior to 0")]
        public int Quantity { get; set; }
    }

    /*In this region I can create restrictions for request that
     the FrontEnd makes me from the Sale Type.*/
    #region Validations
    public class ClientExistsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int idProduct = (int)value;
            try
            {
                using (var db = new Models.Context())
                {
                    if (db.UserClients.Find(idProduct) == null)
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
            }
            return true;
        }
    }
    public class ProductExistsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int idClient = (int)value;
            try
            {
                using (var db = new Models.Context())
                {
                    if (db.Products.Find(idClient) == null)
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
            }
            return true;
        }
    }
    #endregion
}
