using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Domain.Repositories;
using OrderService.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application
{
    public static class ApplicationModule
    {
        public static void AddApplicationMiddlewares(this IServiceCollection services)
        {
            services.ConfigureMediator();
        }

        public static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CreateOrderCommand>());
        }
    }
}
