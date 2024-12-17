namespace Orders.Application.DTOs
{
    public class CreateSessionDTO
    {
        public CreateSessionDTO(string userEmail, string userId, string orderNumber, string productTitle, string productDescription, long orderTotal)
        {
            UserEmail = userEmail;
            UserId = userId;
            OrderNumber = orderNumber;
            ProductTitle = productTitle;
            ProductDescription = productDescription;
            OrderTotal = orderTotal;
        }

        public string UserEmail { get; private set; }
        public string UserId { get; private set; }
        public string OrderNumber { get; private set; }
        public string ProductTitle { get; private set; }
        public string ProductDescription { get; private set; }
        public long OrderTotal { get; private set; }
    }
}