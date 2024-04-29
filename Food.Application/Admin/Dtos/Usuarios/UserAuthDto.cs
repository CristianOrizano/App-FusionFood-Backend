using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.Usuarios
{
    public class UserAuthDto
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
