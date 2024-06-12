using Food.Application.Admin.Dtos.DetalleOrdens;
using Food.Domain.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.Ordens
{
    public class OrdenSaveDto
    {
        public int IdCliente { get; set; }
        public decimal Total { get; set; }
        public string? Direccion { get; set; }
        public string? TipoPago { get; set; }
        public string? Comentario { get; set; }
     

        public ICollection<DetalleOrdenDto>? DetalleOrdens { get; set; }

    }
}
