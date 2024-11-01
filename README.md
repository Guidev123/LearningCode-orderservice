<body>
    <h1>Learning Code - API de Orders 🛒</h1>

   <p>Esta API faz parte do microsserviço <strong>Learning Code</strong>, um software de e-learning. Ela é responsável pelas operações de pedidos de produtos e assinaturas, integrando-se com o <strong>gateway de pagamento Stripe</strong> para criar sessões de checkout. Quando o usuário compra uma assinatura e o pagamento é confirmado no Stripe, a API de pedidos envia uma mensagem via <strong>RabbitMQ</strong> para a API de usuários, solicitando a atualização da <code>Role</code> do usuário.</p>

   <h2>Segurança 👮‍♂️</h2>
    <p>A segurança foi uma prioridade máxima no desenvolvimento desta API. Utilizamos as melhores práticas de segurança, incluindo a autenticação via <strong>JWT</strong> para garantir que apenas usuários autenticados possam acessar os endpoints.</p>

   <h2>Arquitetura</h2>
    <p>A arquitetura da API foi projetada seguindo rigorosamente a <strong>Arquitetura Cebola (Onion Architecture)</strong>, com as camadas:</p>
    <ul>
        <li><strong>API</strong></li>
        <li><strong>Application</strong></li>
        <li><strong>Domain</strong></li>
        <li><strong>Infrastructure</strong></li>
     

   </ul>
    
  ![image](https://github.com/user-attachments/assets/e99cd144-3eb8-4359-9a2d-b3ac3fd826fd)
   <h2>Padrões e Tecnologias</h2>
    <p>Os seguintes padrões e tecnologias foram adotados para o desenvolvimento da API:</p>
    <ul>
        <li><strong>CQRS e MediatR</strong> para manipulação de comandos e consultas.</li>
        <li><strong>Minimal APIs</strong> para otimização de desempenho.</li>
        <li>Desenvolvido em <strong>.NET</strong> com banco de dados <strong>SQL Server</strong> e <strong>Entity Framework</strong> como ORM.</li>
        <li>Integração assíncrona com <strong>RabbitMQ</strong> para comunicação entre serviços.</li>
        <li>Integração com <strong>Stripe</strong> para processar pagamentos e criar sessões de checkout.</li>
    </ul>

   <h2>Endpoints</h2>
 
 ![image](https://github.com/user-attachments/assets/746983b4-0f93-4d10-8220-1c7d77ee97d3)

   
   <h3>Orders</h3>
    <ul>
        <li><strong>POST /api/v1/orders/{id}/cancel</strong>: Cancela um pedido com o <code>id</code> especificado.</li>
        <li><strong>POST /api/v1/orders</strong>: Cria um novo pedido.</li>
        <pre>
{
  "productId": 0,
  "voucherId": 0
}
        </pre>
        <li><strong>GET /api/v1/orders</strong>: Retorna uma lista paginada de pedidos. Requer <code>pageSize</code> e <code>pageNumber</code> como parâmetros.</li>
        <li><strong>GET /api/v1/orders/{number}</strong>: Consulta um pedido específico pelo <code>number</code> do pedido.</li>
        <li><strong>POST /api/v1/orders/{number}/pay</strong>: Realiza o pagamento de um pedido específico.</li>
        <li><strong>POST /api/v1/orders/{id}/refund</strong>: Solicita o reembolso de um pedido específico.</li>
    </ul>

   <h3>Payments</h3>
    <ul>
        <li><strong>GET /api/v1/payments/{number}/transactions</strong>: Consulta transações relacionadas a um pedido específico.</li>
        <li><strong>POST /api/v1/payments/session</strong>: Cria uma sessão de pagamento no Stripe.</li>
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
        <li><strong>GET /api/v1/products</strong>: Retorna uma lista paginada de produtos. Requer <code>pageSize</code> e <code>pageNumber</code> como parâmetros.</li>
        <li><strong>GET /api/v1/products/{slug}</strong>: Consulta um produto específico pelo <code>slug</code>.</li>
    </ul>

   <h3>Vouchers</h3>
    <ul>
        <li><strong>GET /api/v1/vouchers/{number}</strong>: Consulta informações de um voucher específico pelo <code>number</code>.</li>
    </ul>
</body>
