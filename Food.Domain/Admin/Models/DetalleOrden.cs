using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Admin.Models
{
    public class DetalleOrden
    {
        public int Id { get; set; }
        public int IdOrden { get; set; }
        public int IdFood { get; set; }
        public int Cantidad { get; set; }
        public FoodMenu? FoodMenu { get; set; }
        public Orden? Orden { get; set; }
    }
}
