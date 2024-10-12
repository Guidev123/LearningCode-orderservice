using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Repositories;
using OrderService.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure
{
    public static class InfrastructureModule
    {
        public static void AddInfrastructureMiddlewares(this IServiceCollection services, IConfiguration configuration)
        {
            services.PersistenceConfiguration(configuration);
        }
        
        public static void PersistenceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(op =>
                op.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ?? string.Empty));

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IVoucherRepository, VoucherRepository>();

        }
    }
}
