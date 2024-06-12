using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Admin.Dtos.EstadoPedidos;
using Food.Application.Admin.Services;
using Food.Application.Admin.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoPedidoController : ControllerBase
    {
        private readonly IEstadoPedidoService _estadoService;
        public EstadoPedidoController(IEstadoPedidoService estadoService)
        {
            _estadoService = estadoService;
        }


        [HttpGet("listasimple")]
        public async Task<IEnumerable<EstadoPedidoDto>> listsimple()
        {
            return await _estadoService.listaSimpleEstados();
        }


    }
}
