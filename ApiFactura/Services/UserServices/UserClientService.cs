using ApiFactura.Models;
using ApiFactura.Models.Common;
using ApiFactura.Models.Request;
using ApiFactura.Models.Response;
using ApiFactura.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiFactura.Services.UserServices
{
    public class UserClientService : IUserClientService
    {
        private readonly AppSettings _appSettings;
        public UserClientService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public UserResponse Auth(AuthRequest model)
        {
            UserResponse responseUserClient = new UserResponse();
            using (var DB = new Context())
            {
                string encryptedPassword = Encrypt.GetSHA256(model.Password);
                var user = DB.UserClients.Where(u=>
                u.Email == model.Email && u.Password == encryptedPassword)
                    .FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                responseUserClient.Email = user.Email;
                responseUserClient.Token = GetToken(user);
                responseUserClient.ID = user.Id;
            }
            return responseUserClient;
        }
        private string GetToken(UserClient userClient)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userClient.Id.ToString()),
                    new Claim(ClaimTypes.Email, userClient.Email),
                    new Claim(ClaimTypes.Role, "client")
                }),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(llave),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
