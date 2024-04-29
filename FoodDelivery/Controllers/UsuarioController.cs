﻿using Food.Application.Admin.Dtos.Usuarios;
using Food.Application.Admin.Services;
using FoodDelivery.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _userService;
        public UsuarioController(IUsuarioService userService)
        {
            _userService = userService;
        }


        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // POST api/values
        [HttpPost("Login")]
        [AllowAnonymous] //no requiere auth del token
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSecurityDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorValidationModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<BadRequest, Ok<UserSecurityDto>>> Post([FromBody] UserAuthDto userAuth)
        {
            UserSecurityDto userSecurity = await _userService.LoginAsync(userAuth);

            return TypedResults.Ok(userSecurity);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        [AllowAnonymous] //no requiere auth del token
        public async Task<Results<BadRequest, CreatedAtRoute<UsuarioDto>>> Post([FromBody] UsuarioSaveDto userSave)
        {
            UsuarioDto user = await _userService.CreateAsync(userSave);

            return TypedResults.CreatedAtRoute(user);
        }


    }
}
