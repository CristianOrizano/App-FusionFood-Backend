using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Cores.Services
{
    public interface ICrudService<TDto, TDtoSave, ID> : IQueryService<TDto, ID>, ISaveService<TDto, TDtoSave, ID>,
       IDisableService<TDto, ID>
    {
    }
}
