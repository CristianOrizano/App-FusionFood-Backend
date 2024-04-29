using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Core.Models
{
    public class Paging
    {
        private int _pageNumber;

        private int _pageSize;

        public int PageNumber
        {
            get
            {
                return (_pageNumber <= 0) ? 1 : _pageNumber;
            }
            set
            {
                _pageNumber = value;
            }
        }

        public int PageSize
        {
            get
            {
                return (_pageSize <= 0) ? 10 : _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

    }
}
