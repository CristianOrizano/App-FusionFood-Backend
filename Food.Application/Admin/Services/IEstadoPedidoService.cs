using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Admin.Dtos.EstadoPedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Services
{
    public interface IEstadoPedidoService
    {
        Task<IReadOnlyList<EstadoPedidoDto>> listaSimpleEstados();

    }
}
