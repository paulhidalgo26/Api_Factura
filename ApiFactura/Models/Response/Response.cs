namespace ApiFactura.Models.Response
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public Response(){
            this.Success = false;
        }

    }
}
