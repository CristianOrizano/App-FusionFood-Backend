using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Core.Paginations
{
    public class PageResponse<T>
    {
        public IReadOnlyList<T> Data { get; set; }

        public int From { get; set; }

        public int To { get; set; }

        public int PerPage { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public int Total { get; set; }
    }
}
