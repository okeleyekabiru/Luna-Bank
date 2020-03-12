using System;
using System.Collections.Generic;
using System.Text;

namespace Lunabank.Data.ResourceParameters
{
    public class TransactionHistoryParameters
    {
        private const int MaxPageSize = 10;
        private int _pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public DateTime? FromDate { get; set; }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
