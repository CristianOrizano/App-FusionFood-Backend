
using Food.Application.Admin.Dtos.Categorias;
using Food.Application.Cores.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Services
{
    public interface ICategoriaService : ICrudService<CategoriaDto,CategoriaSaveDto,int>, IPageService<CategoriaDto, CategoriaFilterDto>
    {
        Task<IReadOnlyList<CategoriaSimpleDto>> listaSimpleCateoria();
        Task<ActionResult<object>> guadarCategoriaFood(CategoriaFoodSaveDto save);

    }
}
