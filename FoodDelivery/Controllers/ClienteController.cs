using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Admin.Dtos.Clientes;
using Food.Application.Admin.Dtos.Usuarios;
using Food.Application.Admin.Services;
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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        // POST api/values
        [HttpPost("Login")]
        [AllowAnonymous] //no requiere auth del token
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSecurityDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorValidationModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<BadRequest, Ok<ClienteDto>>> Post([FromBody] UserAuthDto userAuth)
        {
            ClienteDto userSecurity = await _clienteService.LoginAsync(userAuth);

            return TypedResults.Ok(userSecurity);
        }



        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ClienteDto>> Get()
        {
            return await _clienteService.FindAllAsync();
        }
      
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<ClienteDto>>> Get(int id)
        {
            var response = await _clienteService.FindByIdAsync(id);
            return TypedResults.Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClienteDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<Results<BadRequest, CreatedAtRoute<ClienteDto>>> Post([FromBody] ClienteSaveDto save)
        {
            var response = await _clienteService.CreateAsync(save);
            return TypedResults.CreatedAtRoute(response);
        }
       
        [HttpPut("{id}")]
        public async Task<ClienteDto> Put(int id, [FromBody] ClienteSaveDto save)
        {
            return await _clienteService.EditAsync(id, save);
        }

     
        [HttpDelete("{id}")]
        public async Task<ClienteDto> Delete(int id)
        {
            return await _clienteService.DisabledAsync(id);

        }

        [HttpGet("busquedaPaginada")]
        public async Task<PageResponse<ClienteDto>> FindAllPaginated([FromQuery] PageRequest<ClienteFilterDto> request)
        {
            return await _clienteService.FindAllPaginatedAsync(request);
        }
    }
}
