using AutoMapper;
using Food.Application.Admin.Dtos.Usuarios;
using Food.Application.Cores.Exceptions;
using Food.Core.Securities.Services;
using Food.Domain.Admin.Models;
using Food.Domain.Admin.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, ISecurityService securityService, IConfiguration configuration) { 

            _usuarioRepository = usuarioRepository;
            _securityService = securityService;
            _configuration = configuration;
            _mapper = mapper;     
        }

        public async Task<UsuarioDto> CreateAsync(UsuarioSaveDto saveDto)
        {
            Usuario user = _mapper.Map<Usuario>(saveDto);
            user.Estado = true;
     
            user.Password = _securityService.HashPassword(saveDto.Email, saveDto.Password);

            await _usuarioRepository.SaveAsync(user);
            return _mapper.Map<UsuarioDto>(user);
        }

        public Task<UsuarioDto> EditAsync(int id, UsuarioSaveDto saveDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserSecurityDto> LoginAsync(UserAuthDto userAuth)
        {
            Usuario? user = await _usuarioRepository.FindByUsername(userAuth.Username);

            if (user is null) throw new NotFoundCoreException("Usuario no esta registrado en nuestro Sistema");

            bool isCorrect = _securityService.VerifyHashedPassword(user.Email, user.Password, userAuth.Password);

            if (!isCorrect) throw new NotFoundCoreException("La contraseña que ingreso no es correcta");

            if (!user.Estado) throw new NotFoundCoreException("Usuario no esta activo. Comuniquese con el adminstrador");

            UserSecurityDto userSecurity = _mapper.Map<UserSecurityDto>(user);

            string jwtSecretKey = _configuration.GetSection("Security:JwtSecrectKey").Get<string>();

            userSecurity.Security = _securityService.JwtSecurity(jwtSecretKey);
            return userSecurity;
        }
    }
}
