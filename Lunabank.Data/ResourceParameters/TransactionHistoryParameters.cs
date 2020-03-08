using System;
using System.Collections.Generic;
using System.Text;

namespace Lunabank.Data.ResourceParameters
{
    public class TransactionHistoryParameters
    {
        private const int maxPageSize = 10;
        private int _pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public DateTime? FromDate { get; set; }

        public int Pagesize
        {
            get => _pageSize;
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}
