<body>
    <h1>Learning Code - Orders API 🛒</h1>

 <p>This API is part of the <strong>Learning Code</strong> microservice, an e-learning software. It is responsible for product and subscription order operations, integrating with the <strong>Stripe payment gateway</strong> to create checkout sessions. When a user purchases a subscription and the payment is confirmed on Stripe, the orders API sends a message via <strong>RabbitMQ</strong> to the users API, requesting an update to the user's <code>Role</code>.</p>


 <h2>Security 👮‍♂️</h2>
    <p>Security was a top priority in the development of this API. We used best security practices, including authentication via <strong>JWT</strong> to ensure that only authenticated users can access the endpoints.</p>

 <h2>Architecture</h2>
 <p>The API architecture was designed by strictly following the <strong>Onion Architecture</strong>, with the following layers:</p>
 <ul>
        <li><strong>API</strong></li>
        <li><strong>Application</strong></li>
        <li><strong>Domain</strong></li>
        <li><strong>Infrastructure</strong></li>
 </ul>
    
<img src="https://github.com/user-attachments/assets/e99cd144-3eb8-4359-9a2d-b3ac3fd826fd" alt="API Architecture">
    
<h2>Patterns and Technologies</h2>
 <p>The following patterns and technologies were adopted for the development of the API:</p>
 <ul>
        <li><strong>CQRS and MediatR</strong> for command and query handling.</li>
        <li><strong>Minimal APIs</strong> for performance optimization.</li>
        <li>Developed in <strong>.NET</strong> with a <strong>SQL Server</strong> database and <strong>Entity Framework</strong> as ORM.</li>
        <li>Asynchronous integration with <strong>RabbitMQ</strong> for communication between services.</li>
        <li>Integration with <strong>Stripe</strong> to process payments and create checkout sessions.</li>
 </ul>

   <h2>Endpoints</h2>
 
 <img src="https://github.com/user-attachments/assets/746983b4-0f93-4d10-8220-1c7d77ee97d3" alt="API Endpoints">

 <h3>Orders</h3>
  <ul>
        <li><strong>POST /api/v1/orders/{id}/cancel</strong>: Cancels an order with the specified <code>id</code>.</li>
        <li><strong>POST /api/v1/orders</strong>: Creates a new order.</li>
        <pre>
{
  "productId": 0,
  "voucherId": 0
}
        </pre>
        <li><strong>GET /api/v1/orders</strong>: Returns a paginated list of orders. Requires <code>pageSize</code> and <code>pageNumber</code> as parameters.</li>
        <li><strong>GET /api/v1/orders/{number}</strong>: Queries a specific order by its <code>number</code>.</li>
        <li><strong>POST /api/v1/orders/{number}/pay</strong>: Processes the payment for a specific order.</li>
        <li><strong>POST /api/v1/orders/{id}/refund</strong>: Requests a refund for a specific order.</li>
    </ul>

 <h3>Payments</h3>
    <ul>
        <li><strong>GET /api/v1/payments/{number}/transactions</strong>: Queries transactions related to a specific order.</li>
        <li><strong>POST /api/v1/payments/session</strong>: Creates a payment session in Stripe.</li>
        <pre>
{
  "orderNumber": "string",
  "productTitle": "string",
  "productDescription": "string",
  "orderTotal": 0
}
        </pre>
    </ul>

<h3>Products</h3>
    <ul>
        <li><strong>GET /api/v1/products</strong>: Returns a paginated list of products. Requires <code>pageSize</code> and <code>pageNumber</code> as parameters.</li>
        <li><strong>GET /api/v1/products/{slug}</strong>: Queries a specific product by its <code>slug</code>.</li>
    </ul>

<h3>Vouchers</h3>
    <ul>
        <li><strong>GET /api/v1/vouchers/{number}</strong>: Queries information about a specific voucher by its <code>number</code>.</li>
    </ul>
</body>
