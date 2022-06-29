using ApiFactura.Models.Request;
using ApiFactura.Models.Response;

namespace ApiFactura.Services.UserServices
{
    public interface IUserAdminService
    {
        UserResponse Auth(AuthRequest model);
    }
}
