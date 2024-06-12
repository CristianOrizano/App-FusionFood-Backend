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
    public class DetalleOrdenConfiguration : IEntityTypeConfiguration<DetalleOrden>
    {
        public void Configure(EntityTypeBuilder<DetalleOrden> builder)
        {
            builder.ToTable("tb_detalle_orden", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.IdOrden).HasColumnName("id_orden");
            builder.Property(x => x.IdFood).HasColumnName("id_food");
            builder.Property(x => x.Cantidad).HasColumnName("cantidad");

            builder.HasOne(x => x.FoodMenu).WithMany(x => x.DetalleOrdens).
         HasForeignKey(x => x.IdFood);

            builder.HasOne(x => x.Orden).WithMany(x => x.DetalleOrdens).
         HasForeignKey(x => x.IdOrden);

        }

    }
}
