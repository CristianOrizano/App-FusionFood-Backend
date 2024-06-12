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
    public class EstadoPedidoRepository : CrudRepository<EstadoPedido, int, ApplicationDbContext>, IEstadoPedidoRepository
    {
        public EstadoPedidoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
