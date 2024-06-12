using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Admin.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? Telefono { get; set; }
        public string? Nimagen { get; set; }
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
        public bool Estado { get; set; }
        public ICollection<Orden>? Ordens { get; set; }

    }
}
