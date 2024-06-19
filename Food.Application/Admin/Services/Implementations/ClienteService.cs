using AutoMapper;
using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Admin.Dtos.Clientes;
using Food.Application.Admin.Dtos.Usuarios;
using Food.Application.Cores.Exceptions;
using Food.Core.Paginations;
using Food.Core.Securities.Services;
using Food.Domain.Admin.Models;
using Food.Domain.Admin.Repository;
using Food.Domain.Core.Models;
using Food.Infraestructura.Admin.Persistences;
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
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(IClienteRepository clienteRepository, ISecurityService securityService, IMapper mapper, ILogger<ClienteService> logger)
        {
            _clienteRepository = clienteRepository;
            _securityService = securityService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ClienteDto> CreateAsync(ClienteSaveDto saveDto)
        {

            Cliente cliente = _mapper.Map<Cliente>(saveDto);
            cliente.Contrasena = _securityService.HashPassword(saveDto.Correo, saveDto.Contrasena);
            cliente.Estado = true;
            Cliente clienteSave = await _clienteRepository.SaveAsync(cliente);

            return _mapper.Map<ClienteDto>(clienteSave);
        }

        public async Task<ClienteDto> DisabledAsync(int id)
        {
            Cliente cliente = await _clienteRepository.FindByIdAsync(id)
                               ?? throw new NotFoundCoreException($"No encontrado para id: {id}");
            cliente.Estado = !cliente.Estado;
            Cliente save = await _clienteRepository.SaveAsync(cliente);

            return _mapper.Map<ClienteDto>(save);
        }

        public async Task<ClienteDto> EditAsync(int id, ClienteSaveDto saveDto)
        {
            Cliente cliente = await _clienteRepository.FindByIdAsync(id);
           
            _mapper.Map<ClienteSaveDto, Cliente>(saveDto, cliente);
            Cliente save = await _clienteRepository.SaveAsync(cliente);

            return _mapper.Map<ClienteDto>(save);
        }

        public async Task<IReadOnlyList<ClienteDto>> FindAllAsync()
        {
            IReadOnlyList<Cliente> categoria = await _clienteRepository.FindAllAsync();
            return _mapper.Map<IReadOnlyList<ClienteDto>>(categoria);
        }

        public async Task<PageResponse<ClienteDto>> FindAllPaginatedAsync(PageRequest<ClienteFilterDto> request)
        {
            var filter = request.Filter ?? new ClienteFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Cliente, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Nombres) || x.Nombres.ToUpper().Contains(filter.Nombres.ToUpper()))
             && (string.IsNullOrWhiteSpace(filter.Apellidos) || x.Apellidos.ToUpper().Contains(filter.Apellidos.ToUpper()))
              && (string.IsNullOrWhiteSpace(filter.Correo) || x.Correo.ToUpper().Contains(filter.Correo.ToUpper()))
               && (filter.Telefono == 0 || x.Telefono == filter.Telefono)
             && (!filter.Estado.HasValue || x.Estado == filter.Estado)
            &&  (filter.FechaNacimiento == DateTime.MinValue || x.FechaNacimiento.Date == filter.FechaNacimiento.Date);
            var response = await _clienteRepository.FindAllPaginatedAsync(paging, predicate);

            return _mapper.Map<PageResponse<ClienteDto>>(response);
        }

        public async Task<ClienteDto> FindByIdAsync(int id)
        {
            Cliente? cliente = await _clienteRepository.FindByIdAsync(id) ??
                       throw new NotFoundCoreException($"cliente no encontrada{id}");

            if (cliente is null)
            {
                _logger.LogWarning("cliente no encontrado para el id: " + id);
                // throw InvesmentNotFound(id);
            }
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<ClienteDto> LoginAsync(UserAuthDto userAuth)
        {
            Cliente? user = await _clienteRepository.FindByIdAsync( x=> x.Correo == userAuth.Username);

            if (user is null) throw new NotFoundCoreException("Cliente no esta registrado en nuestro Sistema");

            bool isCorrect = _securityService.VerifyHashedPassword(user.Correo, user.Contrasena, userAuth.Password);

            if (!isCorrect) throw new NotFoundCoreException("La contraseña que ingreso no es correcta");

            if (!user.Estado) throw new NotFoundCoreException("Cliente no esta activo. Comuniquese con el adminstrador");


            return _mapper.Map<ClienteDto>(user); 
        }
    }
}
