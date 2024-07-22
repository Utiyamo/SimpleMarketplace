using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Domain.Models
{
    public class PaginationResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalRecords { get; set; }
        public IEnumerable<T?> Items { get; set; }

        public PaginationResponse() { }

        public PaginationResponse(int pageNumber, int pageSize, long totalRecords, IEnumerable<T?> data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            Items = data;
        }
    }
}
