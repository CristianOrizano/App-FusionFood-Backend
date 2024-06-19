using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.Ordens
{
    public class OrdenFilterDto
    {
        public DateTime? FechaOrden { get; set; }
        public string? TipoPago { get; set; }
        public int idCliente { get; set; }
        public int Estado { get; set; }

    }
}
