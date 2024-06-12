using AutoMapper;
using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Admin.Dtos.EstadoPedidos;
using Food.Domain.Admin.Models;
using Food.Domain.Admin.Repository;
using Food.Infraestructura.Admin.Persistences;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Services.Implementations
{
    public class EstadoPedidoService : IEstadoPedidoService
    {
        private readonly IEstadoPedidoRepository _estadoPedidoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EstadoPedidoService> _logger;

        public EstadoPedidoService(IEstadoPedidoRepository estadoPedidoRepository, IMapper mapper, ILogger<EstadoPedidoService> logger)
        {
            _estadoPedidoRepository = estadoPedidoRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IReadOnlyList<EstadoPedidoDto>> listaSimpleEstados()
        {
            IReadOnlyList<EstadoPedido> categoria = await _estadoPedidoRepository.FindAllAsync();
            return _mapper.Map<IReadOnlyList<EstadoPedidoDto>>(categoria);
        }
    }
}
