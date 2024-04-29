using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Core.Models
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Data { get; set; }

        public int From { get; set; }

        public int To { get; set; }

        public int PerPage { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public int Total { get; set; }

        public PagedResult(IReadOnlyList<T> data, Paging paging, int totalElements)
        {
            Data = data;
            int num = paging.PageNumber;
            int pageSize = paging.PageSize;
            int num2 = (int)Math.Ceiling((decimal)totalElements / (decimal)pageSize);
            if (num2 < 1)
            {
                num2 = 1;
            }

            int currentPage = num;
            if (num > num2)
            {
                num = num2;
            }

            int num3 = num * pageSize;
            if (num3 > totalElements)
            {
                num3 = totalElements;
            }

            int from = (num - 1) * pageSize + 1;
            if (totalElements <= 0)
            {
                from = 0;
            }

            From = from;
            To = num3;
            PerPage = pageSize;
            CurrentPage = currentPage;
            LastPage = num2;
            Total = totalElements;
        }
    }
}
