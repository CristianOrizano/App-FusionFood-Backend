using AutoMapper;
using Food.Application.Admin.Dtos.Categorias;
using Food.Domain.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.EstadoPedidos.Profiles
{
    public class EstadoProfile : Profile
    {

        public EstadoProfile()
        {
            CreateMap<EstadoPedido, EstadoPedidoDto>().ReverseMap();
        }
    }
}
