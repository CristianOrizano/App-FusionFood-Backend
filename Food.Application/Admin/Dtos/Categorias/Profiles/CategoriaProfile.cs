using AutoMapper;
using Food.Core.Paginations;
using Food.Domain.Admin.Models;
using Food.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.Categorias.Profiles
{
    public class CategoriaProfile: Profile
    {
        public CategoriaProfile() {

            CreateMap<Categoria,CategoriaDto>();
            CreateMap<Categoria, CategoriaSaveDto>().ReverseMap();
            CreateMap<Categoria, CategoriaSimpleDto>().ReverseMap();

            //pagination
            CreateMap<PagedResult<Categoria>, PageResponse<CategoriaDto>>();
            CreateMap<CategoriaFoodSaveDto, Categoria>()
                .ForMember(dest => dest.FoodMenus, opt => opt.MapFrom(src => src.Foods))
                   .AfterMap((src, dest) =>
                   {
                       // Establece el estado a true para cada FoodMenu
                       if (dest.FoodMenus != null)
                       {
                           foreach (var foodMenu in dest.FoodMenus)
                           {
                               foodMenu.Estado = true; // Asigna Estado a true
                           }
                       }
                   });

     
            /*CreateMap<CategoriaFoodSaveDto, Categoria>()
                .ForMember(dest => dest.FoodMenus, opt => opt.MapFrom(src => src.Foods.Select(food => new FoodMenu
                {
                    // Mapeo de propiedades dentro de FoodMenuSaveDto a FoodMenu
                   Precio  = food.Precio,
                    Descripcion = food.Descripcion,
                    Estado = true
                }))); */
        }


    }
}
