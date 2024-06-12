using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Admin.Dtos.DetalleOrdens;
using Food.Application.Admin.Dtos.FoodMenus;
using Food.Application.Admin.Dtos.Ordens;
using Food.Application.Admin.Services;
using Food.Application.Admin.Services.Implementations;
using Food.Core.Paginations;
using FoodDelivery.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenService _ordenService;
        public OrdenController(IOrdenService ordenService)
        {
            _ordenService = ordenService;
        }

        [AllowAnonymous]
        [HttpPost("saveorden")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrdenSaveDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<object>> orden([FromBody] OrdenSaveDto save)
        {

            return await _ordenService.SaveOdenesAsync(save);
        }
        
        [HttpPut("{id}")]
        public async Task<OrdenDto> Put(int id, [FromBody] OrdensSaveDto save)
        {
            return await _ordenService.EditAsync(id, save);
        }


        [HttpGet("buscardetallepor/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetalleOrdenListDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<NotFound, Ok<IReadOnlyList<DetalleOrdenListDto>>>> buscardetallePorId(int id)
        {
            var response = await _ordenService.BuscarDetalleOrdenAsync(id);

            return TypedResults.Ok(response);
        }



        [HttpGet("busquedaPaginada")]
        public async Task<PageResponse<OrdenDto>> FindAllPaginated([FromQuery] PageRequest<OrdenFilterDto> request)
        {
            return await _ordenService.FindAllPaginatedAsync(request);
        }


    }
}
