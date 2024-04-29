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
    public class FoodMenuConfiguration : IEntityTypeConfiguration<FoodMenu>
    {
        public void Configure(EntityTypeBuilder<FoodMenu> builder)
        {
            builder.ToTable("tb_food", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.IdCategoria).HasColumnName("id_categoria");
            builder.Property(x => x.Descripcion).HasColumnName("descripcion");
            builder.Property(x => x.Precio).HasColumnName("precio");
            builder.Property(x => x.Estado).HasColumnName("estado");

            builder.HasOne(x => x.Categoria).WithMany(x => x.FoodMenus).
            HasForeignKey(x => x.IdCategoria);
        }

    }
}
