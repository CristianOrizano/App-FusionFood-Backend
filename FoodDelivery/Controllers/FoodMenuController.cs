using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Admin.Dtos.FoodMenus;
using Food.Application.Admin.Services;
using Food.Application.Admin.Services.Implementations;
using Food.Core.Paginations;
using FoodDelivery.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodMenuController : ControllerBase
    {
        private readonly IFoodMenuService _foodService;
        public FoodMenuController(IFoodMenuService foodService)
        {
            _foodService = foodService;
        }


        // GET: api/<FoodMenuController>
        [HttpGet]
        public async Task<IEnumerable<FoodMenuDto>> Get()
        {
            return await _foodService.FindAllAsync();
        }

        // GET api/<FoodMenuController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodMenuDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<FoodMenuDto>>> Get(int id)
        {
            var response = await _foodService.FindByIdAsync(id);
            return TypedResults.Ok(response);
        }

        // POST api/<FoodMenuController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FoodMenuDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<FoodMenuDto>>> Post([FromBody] FoodMenuSaveDto save)
        {
            var response = await _foodService.CreateAsync(save);
            return TypedResults.CreatedAtRoute(response);
        }

        // PUT api/<FoodMenuController>/5
        [HttpPut("{id}")]
        public async Task<FoodMenuDto> Put(int id, [FromBody] FoodMenuSaveDto save)
        {
            return await _foodService.EditAsync(id, save);
        }

        // DELETE api/<FoodMenuController>/5
        [HttpDelete("{id}")]
        public async Task<FoodMenuDto> Delete(int id)
        {
            return await _foodService.DisabledAsync(id);

        }

        [HttpGet("busquedaPaginada")]
        public async Task<PageResponse<FoodMenuDto>> FindAllPaginated([FromQuery] PageRequest<FoodMenuFilterDto> request)
        {
            return await _foodService.FindAllPaginatedAsync(request);
        }
    }
}
