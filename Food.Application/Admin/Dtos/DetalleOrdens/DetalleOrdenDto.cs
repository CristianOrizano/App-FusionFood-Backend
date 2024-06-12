using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.DetalleOrdens
{
    public class DetalleOrdenDto
    {
        //public int IdOrden { get; set; }
        public int IdFood { get; set; }
        public int Cantidad { get; set; }
    }
}
