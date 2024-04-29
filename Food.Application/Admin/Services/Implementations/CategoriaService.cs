using AutoMapper;
using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Cores.Exceptions;
using Food.Core.Paginations;
using Food.Domain.Admin.Models;
using Food.Domain.Admin.Repository;
using Food.Domain.Core.Models;
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
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriaService> _logger;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper, ILogger<CategoriaService> logger)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CategoriaDto> CreateAsync(CategoriaSaveDto saveDto)
        {
            Categoria categoria = _mapper.Map<Categoria>(saveDto);
            categoria.Estado = true;
            Categoria categoriaSave = await _categoriaRepository.SaveAsync(categoria);

            return _mapper.Map<CategoriaDto>(categoriaSave);
        }

        public async Task<CategoriaDto> DisabledAsync(int id)
        {
            Categoria categoria = await _categoriaRepository.FindByIdAsync(id)
                               ?? throw new NotFoundCoreException($"No encontrado para id: {id}");
            categoria.Estado = !categoria.Estado;
            Categoria save = await _categoriaRepository.SaveAsync(categoria);

            return _mapper.Map<CategoriaDto>(save);
        }

        public async Task<CategoriaDto> EditAsync(int id, CategoriaSaveDto saveDto)
        {
            Categoria categoria = await _categoriaRepository.FindByIdAsync(id);
            //modifica la entidad proporcionada como argumento y, por lo tanto,
            //no es necesario almacenarla explícitamente después del mapeo,categoria ya ha sido actualizada por el método Map
            _mapper.Map<CategoriaSaveDto, Categoria>(saveDto, categoria);
            Categoria save = await _categoriaRepository.SaveAsync(categoria);

            return _mapper.Map<CategoriaDto>(save);
        }

        public async Task<IReadOnlyList<CategoriaDto>> FindAllAsync()
        {
            IReadOnlyList<Categoria> categoria = await _categoriaRepository.FindAllAsync();
            return _mapper.Map<IReadOnlyList<CategoriaDto>>(categoria);
        }

 

        public async Task<PageResponse<CategoriaDto>> FindAllPaginatedAsync(PageRequest<CategoriaFilterDto> request)
        {
            var filter = request.Filter ?? new CategoriaFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Categoria, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Nombre) || x.Nombre.ToUpper().Contains(filter.Nombre.ToUpper()))
             && (string.IsNullOrWhiteSpace(filter.Descripcion) || x.Descripcion.ToUpper().Contains(filter.Descripcion.ToUpper()));

            var response = await _categoriaRepository.FindAllPaginatedAsync(paging, predicate);

            return _mapper.Map<PageResponse<CategoriaDto>>(response);
        }

        public async Task<CategoriaDto> FindByIdAsync(int id)
        {
            Categoria? categoria = await _categoriaRepository.FindByIdAsync(id) ?? 
                      throw new NotFoundCoreException($"Categoria no encontrada{id}");

            if (categoria is null)
            {
                _logger.LogWarning("categoria no encontrado para el id: " + id);
               // throw InvesmentNotFound(id);
            }
            return _mapper.Map<CategoriaDto>(categoria);
        }

        public async Task<ActionResult<object>> guadarCategoriaFood(CategoriaFoodSaveDto save)
        {
            Categoria categoria = _mapper.Map<Categoria>(save);
            categoria.Estado = true;
            Categoria categoriaSave = await _categoriaRepository.SaveAsync(categoria);
            if (categoriaSave != null)
            {
                return new { mensaje = "Se ha guardado correctamente." };
            }
            else
            {
                return new { mensaje = "No se pudo guardar la categoría." };
            }
        }

        public async Task<IReadOnlyList<CategoriaSimpleDto>> listaSimpleCateoria()
        {
            IReadOnlyList<Categoria> categoria = await _categoriaRepository.FindAllAsync();
            return _mapper.Map<IReadOnlyList<CategoriaSimpleDto>>(categoria);
        }
    }
}
