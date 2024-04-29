using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.FoodMenus
{
    public class FoodMenuSaveDto
    {
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }

    }
}
