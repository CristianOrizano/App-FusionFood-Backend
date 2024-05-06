using AutoMapper;
using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Admin.Dtos.FoodMenus;
using Food.Application.Cores.Exceptions;
using Food.Core.Paginations;
using Food.Domain.Admin.Models;
using Food.Domain.Admin.Repository;
using Food.Domain.Core.Models;
using Food.Infraestructura.Admin.Persistences;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Services.Implementations
{
    public class FoodMenuService : IFoodMenuService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<FoodMenuService> _logger;

        public FoodMenuService(IFoodRepository foodRepository, IMapper mapper, ILogger<FoodMenuService> logger)
        {
            _foodRepository = foodRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<FoodMenuDto> CreateAsync(FoodMenuSaveDto saveDto)
        {
            FoodMenu foodMenu = _mapper.Map<FoodMenu>(saveDto);
            foodMenu.Estado = true;
            FoodMenu foodMenuSave = await _foodRepository.SaveAsync(foodMenu);

            return _mapper.Map<FoodMenuDto>(foodMenuSave);
        }

        public async Task<FoodMenuDto> DisabledAsync(int id)
        {
            FoodMenu foodMenu = await _foodRepository.FindByIdAsync(id)
                               ?? throw new NotFoundCoreException($"No encontrado para id: {id}");
            foodMenu.Estado = !foodMenu.Estado;
            FoodMenu save = await _foodRepository.SaveAsync(foodMenu);

            return _mapper.Map<FoodMenuDto>(save);
        }

        public async Task<FoodMenuDto> EditAsync(int id, FoodMenuSaveDto saveDto)
        {
            FoodMenu foodMenu = await _foodRepository.FindByIdAsync(id);
 
            _mapper.Map<FoodMenuSaveDto, FoodMenu>(saveDto, foodMenu);
            FoodMenu save = await _foodRepository.SaveAsync(foodMenu);

            return _mapper.Map<FoodMenuDto>(save);
        }

        public async Task<IReadOnlyList<FoodMenuDto>> FindAllAsync()
        {
            List<Expression<Func<FoodMenu, object>>> includes = new List<Expression<Func<FoodMenu, object>>>
            {
                x => x.Categoria,       
            };
            IReadOnlyList<FoodMenu> foodMenu = await _foodRepository.FindAllAsync(null,null,includes,true);
            return _mapper.Map<IReadOnlyList<FoodMenuDto>>(foodMenu);
        }

        public async Task<PageResponse<FoodMenuDto>> FindAllPaginatedAsync(PageRequest<FoodMenuFilterDto> request)
        {
            var filter = request.Filter ?? new FoodMenuFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<FoodMenu, bool>> predicate = x =>
             (string.IsNullOrWhiteSpace(filter.Descripcion) || x.Descripcion.ToUpper().Contains(filter.Descripcion.ToUpper()))
              && (filter.IdCategoria == 0 || x.IdCategoria == filter.IdCategoria)
               && (filter.Precio == 0 || x.Precio == filter.Precio);

            List<Expression<Func<FoodMenu, object>>> includes = new List<Expression<Func<FoodMenu, object>>>
            {
                x => x.Categoria,
            };

            /* && (!filter.FechaEmisionDesde.HasValue || x.Expediente.FechaRegistro.Date >= filter.FechaEmisionDesde.Value.Date)
             && (!filter.FechaEmisionHasta.HasValue || x.Expediente.FechaRegistro.Date <= filter.FechaEmisionHasta.Value.Date);*/
            var response = await _foodRepository.FindAllPaginatedAsync(paging, predicate,null,includes);

            return _mapper.Map<PageResponse<FoodMenuDto>>(response);
        }

        public async Task<FoodMenuDto> FindByIdAsync(int id)
        {
            List<Expression<Func<FoodMenu, object>>> includes = new List<Expression<Func<FoodMenu, object>>>
            {
                x => x.Categoria,
            };

            FoodMenu? foodMenu = await _foodRepository.FindByIdAsync(x=>x.Id==id, includes) ??
                      throw new NotFoundCoreException($"foodMenu no encontrada{id}");

            if (foodMenu is null)
            {
                _logger.LogWarning("foodMenu no encontrado para el id: " + id);
                // throw InvesmentNotFound(id);
            }
            return _mapper.Map<FoodMenuDto>(foodMenu);
        }
    }
}
