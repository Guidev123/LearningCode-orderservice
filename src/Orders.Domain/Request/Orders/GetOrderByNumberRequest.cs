﻿namespace Orders.Domain.Request.Orders
{
    public class GetOrderByNumberRequest : Request
    {
        public string Number { get; set; } = string.Empty;
    }
}
