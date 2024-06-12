using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Admin.Dtos.Clientes;
using Food.Application.Admin.Dtos.Usuarios;
using Food.Application.Cores.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Services
{
    public interface IClienteService : ICrudService<ClienteDto, ClienteSaveDto, int>, IPageService<ClienteDto, ClienteFilterDto>
    {
        Task<ClienteDto> LoginAsync(UserAuthDto userAuth);

    }
}
