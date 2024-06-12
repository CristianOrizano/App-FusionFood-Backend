using Food.Domain.Admin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Infraestructura.Admin.Configuration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("tb_cliente", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Nombres).HasColumnName("nombres");
            builder.Property(x => x.Apellidos).HasColumnName("apellidos");
            builder.Property(x => x.FechaNacimiento).HasColumnName("fecha_nacimiento");
            builder.Property(x => x.Telefono).HasColumnName("telefono");
            builder.Property(x => x.Nimagen).HasColumnName("nimagen");
            builder.Property(x => x.Correo).HasColumnName("correo");
            builder.Property(x => x.Contrasena).HasColumnName("contraseña");
            builder.Property(x => x.Estado).HasColumnName("estado");
        }


    }
}
