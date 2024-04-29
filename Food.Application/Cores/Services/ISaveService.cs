using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Cores.Services
{
    public interface ISaveService<TDto, TDtoSave, ID>
    {
        Task<TDto> CreateAsync(TDtoSave saveDto);
        Task<TDto> EditAsync(ID id, TDtoSave saveDto);
    }
}
