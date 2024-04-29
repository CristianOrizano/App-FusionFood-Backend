using Food.Domain.Admin.Models;
using Food.Domain.Admin.Repository;
using Food.Infraestructura.Core.Contexts;
using Food.Infraestructura.Core.Persistences;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Infraestructura.Admin.Persistences
{
    public class UsuarioRepository : CrudRepository<Usuario, int, ApplicationDbContext>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Usuario> FindByUsername(string username)
        {
            return await _dbContext.Set<Usuario>()
               .Where(t => t.Username.ToUpper().Equals(username.ToUpper()))
               .FirstOrDefaultAsync();
        }
    }
}
