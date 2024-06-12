using AutoMapper;
using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Admin.Dtos.FoodMenus;
using Food.Core.Paginations;
using Food.Domain.Admin.Models;
using Food.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.Ordens.Profiles
{
    public class OrdenProfile: Profile
    {
        public OrdenProfile() {
            CreateMap<Orden, OrdenSaveDto>().ReverseMap();
            CreateMap<Orden, OrdenDto>().ReverseMap();
            CreateMap<Orden, OrdensSaveDto>().ReverseMap();
            //pagination
            CreateMap<PagedResult<Orden>, PageResponse<OrdenDto>>();
        }

    }
}
