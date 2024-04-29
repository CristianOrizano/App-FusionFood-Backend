using Food.Core.Securities.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Core.Securities.Services
{
    public interface ISecurityService
    {
        string HashPassword(string userName, string hashedPassword);
        bool VerifyHashedPassword(string userName, string hashedPassword, string providerPassword);

        SecurityEntity JwtSecurity(string jwtSecrectKey);

    }
}
