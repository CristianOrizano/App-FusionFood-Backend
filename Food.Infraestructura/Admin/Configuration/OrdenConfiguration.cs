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
    public class OrdenConfiguration : IEntityTypeConfiguration<Orden>
    {
        public void Configure(EntityTypeBuilder<Orden> builder)
        {
            builder.ToTable("tb_ordenes", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.FechaOrden).HasColumnName("fecha_orden");
            builder.Property(x => x.IdCliente).HasColumnName("id_cliente");
            builder.Property(x => x.Total).HasColumnName("total");
            builder.Property(x => x.Direccion).HasColumnName("direccion");
            builder.Property(x => x.TipoPago).HasColumnName("tipo_pago");
            builder.Property(x => x.Comentario).HasColumnName("comentario");
            builder.Property(x => x.Estado).HasColumnName("estado");

            builder.HasOne(x => x.EstadoPedido).WithMany(x => x.Ordens).
            HasForeignKey(x => x.Estado);

            builder.HasOne(x => x.Cliente).WithMany(x => x.Ordens).
           HasForeignKey(x => x.IdCliente);
        }
    }
}
