using System;
using System.Collections.Generic;

namespace ApiFactura.Models
{
    public partial class UserAdmin
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Cedula { get; set; } = null!;
    }
}
