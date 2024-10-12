using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderService.Application.Responses
{
    public class PagedResponse<TData> : Response<TData>
    {
        private const int DEFAULT_STATUS_CODE = 200;
        private const int DEFAULT_PAGE_SIZE = 25;
        [JsonConstructor]
        public PagedResponse(
            TData? data,
            int totalCount,
            int currentPage = 1,
            int pageSize = DEFAULT_PAGE_SIZE)
            : base(data)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public PagedResponse(
            TData? data,
            int code = DEFAULT_STATUS_CODE,
            string? message = null)
            : base(data, code, message)
        {
        }

        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;
        public int TotalCount { get; set; }
    }
}
