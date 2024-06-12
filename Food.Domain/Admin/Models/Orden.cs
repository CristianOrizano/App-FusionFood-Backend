using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Admin.Models
{
    public class Orden
    {
        public int Id { get; set; }
        public DateTime FechaOrden { get; set; }
        public int IdCliente { get; set; }
        public decimal Total { get; set; }
        public string? Direccion { get; set; }
        public string? TipoPago { get; set; }
        public string? Comentario { get; set; }
        public int Estado { get; set; }
        public EstadoPedido? EstadoPedido { get; set; }
        public Cliente? Cliente { get; set; }

        public ICollection<DetalleOrden>? DetalleOrdens { get; set; }

    }
}
