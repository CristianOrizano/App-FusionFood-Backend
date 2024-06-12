using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.FoodMenus
{
    public class FoodMenuFilterDto
    {
        public int IdCategoria { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string? SortOrderPrecio { get; set; } // "asc" o "desc"
        public string? SortOrderNombre { get; set; } // "asc" o "desc"
    }
}
