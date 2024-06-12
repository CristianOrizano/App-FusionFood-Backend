using Food.Domain.Admin.Models;
using Food.Domain.Admin.Repository;
using Food.Infraestructura.Core.Contexts;
using Food.Infraestructura.Core.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Infraestructura.Admin.Persistences
{
    public class DetalleOrdenRepository : CrudRepository<DetalleOrden, int, ApplicationDbContext>, IDetalleOrdenRepository
    {
        public DetalleOrdenRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
