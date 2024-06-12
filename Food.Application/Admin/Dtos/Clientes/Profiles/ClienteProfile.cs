using AutoMapper;
using Food.Application.Admin.Dtos.FoodMenus;
using Food.Core.Paginations;
using Food.Domain.Admin.Models;
using Food.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.Clientes.Profiles
{
    public class ClienteProfile: Profile
    {
        public ClienteProfile()
        {

            CreateMap<Cliente, ClienteDto>();
            CreateMap<Cliente, ClienteSaveDto>().ReverseMap();
            //pagination
            CreateMap<PagedResult<Cliente>, PageResponse<ClienteDto>>();
        }
    }
}
