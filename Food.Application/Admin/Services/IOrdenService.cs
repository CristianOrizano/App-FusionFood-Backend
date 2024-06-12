using Food.Application.Admin.Dtos.DetalleOrdens;
using Food.Application.Admin.Dtos.FoodMenus;
using Food.Application.Admin.Dtos.Ordens;
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
    public interface IOrdenService: ICrudService<OrdenDto, OrdensSaveDto, int>, IPageService<OrdenDto, OrdenFilterDto>
    {
        Task<ActionResult<object>> SaveOdenesAsync(OrdenSaveDto save);

        Task<IReadOnlyList<DetalleOrdenListDto>> BuscarDetalleOrdenAsync(int Idorden);
    }
}
