using ApiFactura.Models.Common;
using ApiFactura.Services;
using ApiFactura.Services.ClientsServices;
using ApiFactura.Services.ProductServices;
using ApiFactura.Services.SaleServices;
using ApiFactura.Services.ShoppingCartServices;
using ApiFactura.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region My Cors
var MyAllowSpecificOrigins = "MiCors";
    builder.Services.AddCors(options =>
    {
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("*");
                          builder.WithHeaders("*");
                          builder.WithMethods("*");
                      });
    });
#endregion
// Add services to the container.
builder.Services.AddControllers();

#region JWT
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

//JWT
var appSettings = appSettingsSection.Get<AppSettings>();
var llave = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(d =>
    {
        d.RequireHttpsMetadata = false;
        d.SaveToken = true;
        d.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(llave),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

#region My services
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IUserClientService, UserClientService>();
builder.Services.AddScoped<IUserAdminService, UserAdminService>();
builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IProductService, ProductService>();
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
#region My final declarations
//Use of MyCors
app.UseCors(MyAllowSpecificOrigins);
//Use JWT
app.UseAuthentication();
#endregion
app.UseAuthorization();
app.MapControllers();
app.Run();