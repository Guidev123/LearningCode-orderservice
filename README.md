 <h1 align="center">Microsserviço de Pedidos</h1>
    <p align="center">
        Este projeto é um microsserviço responsável pela gestão de pedidos, com integração ao Stripe para processamento de pagamentos. O sistema é estruturado em três camadas: API, Domain e Infrastructure.
       <img  src="https://github.com/user-attachments/assets/6cbf6f1d-1778-4491-9a2c-f19af758179b" />

  </p>

<div align="center">
   <h2>Estrutura do Projeto</h2>
   <img  src="https://github.com/user-attachments/assets/dd77efd6-e6c5-4c73-86ca-09d98ac1f5b9" />
</div>

<hr/>

  <h3 align="center">Camada API</h3>
    <p align="center">
        Esta camada é responsável pelos endpoints e middlewares.
    </p>

   <h4>Endpoints</h4>
    <ul>
        <li><strong>Pedidos</strong>
            <ul>
                <li><code>POST /orders/cancel</code> - Cancela um pedido.</li>
                <li><code>POST /orders</code> - Cria um novo pedido.</li>
                <li><code>GET /orders</code> - Obtém todos os pedidos com paginação.</li>
                <li><code>GET /orders/{orderNumber}</code> - Obtém um pedido pelo número.</li>
                <li><code>POST /orders/{orderId}/pay</code> - Realiza o pagamento de um pedido.</li>
                <li><code>POST /orders/{orderId}/refund</code> - Solicita o reembolso de um pedido.</li>
            </ul>
        </li>
        <li><strong>Payments</strong>
            <ul>
                <li><code>GET /payments/{number}/transactions</code> - Obtem a transação do stripe pelo numero do pedido.</li>
                <li><code>POST /payments/session</code> - Cria sessão no stripe.</li>
            </ul>
        </li>
        <li><strong>Produtos</strong>
            <ul>
                <li><code>GET /products</code> - Obtém todos os produtos com paginação.</li>
                <li><code>GET /products/{slug}</code> - Obtém um produto pelo slug.</li>
            </ul>
        </li>
        <li><strong>Vouchers</strong>
            <ul>
                <li><code>GET /vouchers/{voucherNumber}</code> - Obtém um voucher pelo número.</li>
            </ul>
        </li>
    </ul>
<hr/>

   <h3 align="center">Camada Domain</h3>
    <p align="center">
        Esta camada contém a lógica de negócio e as entidades.
    </p>

  <h4>Entidades</h4>
    <ul>
        <li>Order</li>
        <li>Product</li>
        <li>Voucher</li>
    </ul>

  <h4>Outros Componentes</h4>
    <ul>
        <li>Enums</li>
        <li>Interfaces
            <ul>
                <li>Interfaces de Services, ExternalServices e Repositories</li>
            </ul>
        </li>
        <li>Requests</li>
        <li>Responses</li>
        <li>Services
            <ul>
                <li>Implementações dos serviços</li>
            </ul>
        </li>
    </ul>
<hr/>
  <h3 align="center">Camada Infrastructure</h3>
    <p align="center">
        Esta camada lida com a comunicação externa e a persistência de dados.
    </p>

  <h4>Componentes</h4>
    <ul>
        <li><strong>ExternalServices</strong>
            <ul>
                <li><code>StripeService</code>: Serviço para interação com a API do Stripe.</li>
            </ul>
        </li>
        <li><strong>Models</strong>
            <ul>
                <li><code>StripeConfigurationSettings</code>: Configurações necessárias para a integração com o Stripe.</li>
            </ul>
        </li>
        <li><strong>Persistence</strong>
            <ul>
                <li>Mapeamento do banco de dados.</li>
                <li>Implementação de Repositories.</li>
                <li>Migrações do banco de dados.</li>
            </ul>
        </li>
        <li><strong>DbContext</strong>
            <ul>
                <li>Contexto do Entity Framework para a persistência de dados.</li>
            </ul>
        </li>
    </ul>

  <h1>Arquitetura e Fluxograma</h1>

          
  ![AYc3o9il0nNSAAAAAElFTkSuQmCC](https://github.com/user-attachments/assets/de2bff55-b337-4dab-9f2d-9effa6281351)
  
  
   ![Fluxograma Pedido Online Amarelo Marrom](https://github.com/user-attachments/assets/6a537f55-6c90-4617-b8d6-ee41df37d44f)
