using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Admin.Models
{
    public class FoodMenu
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }

        public Categoria? Categoria { get; set;}

    }
}
