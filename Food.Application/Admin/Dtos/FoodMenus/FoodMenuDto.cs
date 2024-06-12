using Food.Application.Admin.Dtos.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.FoodMenus
{
    public class FoodMenuDto
    {
        public int Id { get; set; }
        public CategoriaSimpleDto? Categoria { get; set; }
        //public int IdCategoria { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? nombreImg { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }

    }
}
