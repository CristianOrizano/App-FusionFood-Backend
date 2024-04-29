using AutoMapper;
using Food.Application.Admin.Dtos.Categorias;
using Food.Core.Paginations;
using Food.Domain.Admin.Models;
using Food.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.FoodMenus.Profiles
{
    public class FoodMenuProfile : Profile
    {
        public FoodMenuProfile() {

            CreateMap<FoodMenu,FoodMenuDto>();
            CreateMap<FoodMenu, FoodMenuSaveDto>().ReverseMap();
            //pagination
            CreateMap<PagedResult<FoodMenu>, PageResponse<FoodMenuDto>>();
        }    

    }
}
