using Food.Core.Securities.Entitys;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Food.Core.Securities.Services.Implements
{
    public class SecurityService : ISecurityService
    {
        //metodo para encriptar la contraseña
        //el userName en el proceso de hashing, se agrega una capa adicional de seguridad al garantizar que 
        //incluso contraseñas idénticas generen hashes diferentes en
        public string HashPassword(string userName, string hashedPassword)
        {
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

            return passwordHasher.HashPassword(userName, hashedPassword);
        }

        //este método se utiliza para verificar si una contraseña proporcionada por un usuario
        //coincide con la contraseña almacenada en la base de datos como un hash seguro
        public bool VerifyHashedPassword(string userName, string hashedPassword, string providedPassword)
        {

            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(userName, hashedPassword, providedPassword);

            if (result == PasswordVerificationResult.Success) return true;

            return false;
        }
        // se encarga de generar un token JWT firmado con los claims y la fecha de vencimiento especificados
        public SecurityEntity JwtSecurity(string jwtSecrectKey)
        {
            DateTime utcNow = DateTime.UtcNow;

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString()),
            };

            DateTime expireDateTime = utcNow.AddDays(1);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            // Key + credentials

            byte[] key = Encoding.ASCII.GetBytes(jwtSecrectKey);
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(key);
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                expires: expireDateTime,
                notBefore: utcNow,
                signingCredentials: signingCredentials
                );

            string token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);


            return new SecurityEntity()
            {
                TokenType = "Bearer",
                AccesToken = token,
                ExpireOn = expireDateTime
            };
        }

    }
}
