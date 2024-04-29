using Azure;
using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Admin.Services;
using Food.Core.Paginations;
using Food.Domain.Admin.Models;
using FoodDelivery.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // GET: api/<CategoriaController>
        [HttpGet]
        public async Task<IEnumerable<CategoriaDto>> Get()
        {
            return await _categoriaService.FindAllAsync();
        }
        [HttpGet("listasimple")]
        public async Task<IEnumerable<CategoriaSimpleDto>> listsimple()
        {
            return await _categoriaService.listaSimpleCateoria();
        }


        // GET api/<CategoriaController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoriaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<CategoriaDto>>> Get(int id)
        {
            var response= await _categoriaService.FindByIdAsync(id);
            return TypedResults.Ok(response);
        }

        // POST api/<CategoriaController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoriaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<CategoriaDto>>> Post([FromBody] CategoriaSaveDto save)
        {
            var response = await _categoriaService.CreateAsync(save);
            return TypedResults.CreatedAtRoute(response);     
        }
        [HttpPost("categoriaFood")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoriaFoodSaveDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<object>> categoriaFood([FromBody] CategoriaFoodSaveDto save)
        {
         
            return await _categoriaService.guadarCategoriaFood(save);
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public async Task<CategoriaDto> Put(int id, [FromBody] CategoriaSaveDto save)
        {
            return await _categoriaService.EditAsync(id, save);
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public async Task<CategoriaDto> Delete(int id)
        {
            return await _categoriaService.DisabledAsync(id);

        }

        [HttpGet("busquedaPaginada")]
        public async Task<PageResponse<CategoriaDto>> FindAllPaginated([FromQuery] PageRequest<CategoriaFilterDto> request)
        {
            return await _categoriaService.FindAllPaginatedAsync(request);
        }
    }
}
