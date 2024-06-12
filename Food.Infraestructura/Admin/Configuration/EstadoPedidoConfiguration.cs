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
    public class EstadoPedidoConfiguration : IEntityTypeConfiguration<EstadoPedido>
    {

        public void Configure(EntityTypeBuilder<EstadoPedido> builder)
        {
            builder.ToTable("tb_estado_pedido", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Nombre).HasColumnName("nombre");
        }

    }
}
