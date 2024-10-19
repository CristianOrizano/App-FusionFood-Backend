using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Admin.Dtos.FoodMenus;
using Food.Application.Cores.Services;
using Food.Infraestructura.Admin.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Services
{
    public interface IFoodMenuService : ICrudService<FoodMenuDto, FoodMenuSaveDto, int>, IPageService<FoodMenuDto, FoodMenuFilterDto>
    {
        Task<ActionResult<object>> guadarFoodWithCategoria(FoodCategoriaSave save);
    }
}
