using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.Clientes
{
    public class ClienteSaveDto
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? Telefono { get; set; }
        public string? Nimagen { get; set; }
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }

    }
}
