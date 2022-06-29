using System;
using System.Collections.Generic;

namespace ApiFactura.Models
{
    public partial class UserClient
    {
        public UserClient()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Cedula { get; set; } = null!;

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
