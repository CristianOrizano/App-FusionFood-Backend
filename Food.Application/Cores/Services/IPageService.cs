using Food.Core.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Cores.Services
{
    public interface IPageService<TDto, TDtoFilter>
    {
        Task<PageResponse<TDto>> FindAllPaginatedAsync(PageRequest<TDtoFilter> request);
    }
}
