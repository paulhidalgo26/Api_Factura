using ApiFactura.Models.Request;
using ApiFactura.Models.Response;

namespace ApiFactura.Services.ClientsServices
{
    public interface IClientsService
    {
        public Response Get()
        {
            return this.Get();
        }
        public Response Add(NewClientRequest client)
        {
            return this.Add(client);
        }
        public Response UpdateClient(UpdateUserRequest request, int id)
        {
            return this.UpdateClient(request, id);
        }
        public Response DeleteClient(int id)
        {
            return this.DeleteClient(id);
        }
    }
}
