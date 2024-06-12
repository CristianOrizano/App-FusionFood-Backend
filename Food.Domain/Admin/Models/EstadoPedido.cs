using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Admin.Models
{
    public class EstadoPedido
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public ICollection<Orden>? Ordens { get; set; }
    }
}
