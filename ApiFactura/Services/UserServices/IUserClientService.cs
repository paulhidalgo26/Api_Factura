using ApiFactura.Models.Request;
using ApiFactura.Models.Response;

namespace ApiFactura.Services.UserServices
{
    public interface IUserClientService
    {
        UserResponse Auth(AuthRequest model);
    }
}
