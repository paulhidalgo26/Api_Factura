using System.ComponentModel.DataAnnotations;

namespace ApiFactura.Models.Request
{
    public class AuthRequest
    {
        [Required]
        //[EmailExists(ErrorMessage="Email doesn't exists")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
    /*
    public class EmailExistsAttribute: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string email = value.ToString();
            using (var db = new Models.AthanasiaContext())
            {
                var userListClient = db.UserClients.ToList();
                foreach (var user in userListClient)
                {
                    if (user.Email == email)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    */
}
