﻿using Food.Domain.Admin.Models;
using Food.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Admin.Repository
{
    public interface IEstadoPedidoRepository : ICrudRepository<EstadoPedido, int>
    {


    }
}
