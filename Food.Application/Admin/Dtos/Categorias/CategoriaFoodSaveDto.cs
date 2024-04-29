using Food.Application.Admin.Dtos.FoodMenus;
using Food.Domain.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.Categorias
{
    public class CategoriaFoodSaveDto
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public ICollection<FoodMenuSaveDto>? Foods { get; set; }

    }
}
