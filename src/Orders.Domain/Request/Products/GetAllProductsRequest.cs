using Orders.Domain.Request;

namespace Orders.Domain.Request.Products
{
    public class GetAllProductsRequest
    {
        public GetAllProductsRequest(int pageNumber, int pageSize)
        {
            PageNumber = 1;
            PageSize = 25;
        }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
    }
}
