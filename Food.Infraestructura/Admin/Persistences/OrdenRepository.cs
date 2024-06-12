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
    public class OrdenRepository : CrudRepository<Orden, int, ApplicationDbContext>, IOrdenRepository
    {
        public OrdenRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
