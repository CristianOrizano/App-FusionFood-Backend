﻿using Food.Application.Admin.Dtos.Usuarios;
using Food.Application.Cores.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Services
{
    public interface IUsuarioService : ISaveService<UsuarioDto, UsuarioSaveDto, int>
    {
        Task<UserSecurityDto> LoginAsync(UserAuthDto userAuth);
    }
}
