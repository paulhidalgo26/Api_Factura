using ApiFactura.Models.Request;
using ApiFactura.Models.Response;

namespace ApiFactura.Services
{
    public interface ISaleService
    {
        public Response Add(SaleRequest model) {
            return this.Add(model);
        }
    }
}
