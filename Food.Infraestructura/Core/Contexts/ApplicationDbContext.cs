using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Food.Infraestructura.Core.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        //se utilizan para configurar la conexión de base de datos y otros aspectos del contexto.
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //cualquier configuración predeterminada proporcionada por Entity Framework Core se aplique correctamente.
            base.OnModelCreating(modelBuilder);

            //aplicar configuraciones de entidades desde un ensamblaje
            //para aplicar automaticamente todas las configuraciones(config) de entidad definidas en el proyecto al modelo.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
