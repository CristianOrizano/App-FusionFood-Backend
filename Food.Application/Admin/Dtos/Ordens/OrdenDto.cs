using Food.Application.Admin.Dtos.Clientes;
using Food.Application.Admin.Dtos.EstadoPedidos;
using Food.Domain.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.Ordens
{
    public class OrdenDto
    {
        public int Id { get; set; }
        public DateTime FechaOrden { get; set; }
        public ClienteDto? Cliente { get; set; }
        public decimal Total { get; set; }
        public string? Direccion { get; set; }
        public string? TipoPago { get; set; }
        public string? Comentario { get; set; }
        public EstadoPedidoDto? EstadoPedido { get; set; }
    }
}
