using AutoMapper;
using Azure;
using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Admin.Dtos.DetalleOrdens;
using Food.Application.Admin.Dtos.FoodMenus;
using Food.Application.Admin.Dtos.Ordens;
using Food.Application.Cores.Exceptions;
using Food.Core.Paginations;
using Food.Domain.Admin.Models;
using Food.Domain.Admin.Repository;
using Food.Domain.Core.Models;
using Food.Infraestructura.Admin.Persistences;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Services.Implementations
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IDetalleOrdenRepository _detalleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdenService> _logger;

        public OrdenService(IOrdenRepository ordenRepository, IDetalleOrdenRepository detalleRepository, IMapper mapper, ILogger<OrdenService> logger)
        {
            _detalleRepository = detalleRepository;
            _ordenRepository = ordenRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyList<DetalleOrdenListDto>> BuscarDetalleOrdenAsync(int Idorden)
        {
            Expression<Func<DetalleOrden, bool>> predicate = x => x.IdOrden == Idorden;

            List<Expression<Func<DetalleOrden, object>>> includes = new List<Expression<Func<DetalleOrden, object>>>
            {
                x => x.FoodMenu,
               
            };
            var detalles = await _detalleRepository.FindAllAsync(predicate,null,includes);


            return _mapper.Map<IReadOnlyList<DetalleOrdenListDto>>(detalles);
        }

        public Task<OrdenDto> CreateAsync(OrdensSaveDto saveDto)
        {
            throw new NotImplementedException();
        }

        public Task<OrdenDto> DisabledAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrdenDto> EditAsync(int id, OrdensSaveDto saveDto)
        {
            Orden orden = await _ordenRepository.FindByIdAsync(id);

            _mapper.Map<OrdensSaveDto, Orden>(saveDto, orden);
            Orden save = await _ordenRepository.SaveAsync(orden);

            return _mapper.Map<OrdenDto>(save);
        }

        public Task<IReadOnlyList<OrdenDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PageResponse<OrdenDto>> FindAllPaginatedAsync(PageRequest<OrdenFilterDto> request)
        {
            var filter = request.Filter ?? new OrdenFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Orden, bool>> predicate = x =>
                (!filter.FechaOrden.HasValue || x.FechaOrden.Date >= filter.FechaOrden.Value.Date)
             && (string.IsNullOrWhiteSpace(filter.TipoPago) || x.TipoPago.ToUpper().Contains(filter.TipoPago.ToUpper()))
             && (filter.Estado == 0 || x.Estado == filter.Estado);

            List<Expression<Func<Orden, object>>> includes = new List<Expression<Func<Orden, object>>>
            {
                x => x.EstadoPedido,
                x => x.Cliente

            };
     
            Func<IQueryable<Orden>, IOrderedQueryable<Orden>> orderBy = query => query.OrderByDescending(x => x.FechaOrden);

            var response = await _ordenRepository.FindAllPaginatedAsync(paging, predicate, orderBy, includes);

            return _mapper.Map<PageResponse<OrdenDto>>(response);
        }

        public Task<OrdenDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<object>> SaveOdenesAsync(OrdenSaveDto save)
        {
            Orden orden = _mapper.Map<Orden>(save);
            orden.FechaOrden = DateTime.Now;
            orden.Estado = 1; // pendiente
            Orden saveorden =  await _ordenRepository.SaveAsync(orden);

            if (saveorden != null)
            {       
                return new { message = "Orden guardada exitosamente"};
            }
            else
            {
                // Return an error message if the save operation did not succeed
                 throw new NotFoundCoreException($"Orden no registrada");
            }
        }
    }
}
