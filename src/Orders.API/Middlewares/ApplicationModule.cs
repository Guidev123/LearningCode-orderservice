﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Orders.API.Services;
using Orders.Domain.Interfaces.ExternalServices;
using Orders.Domain.Interfaces.Repositories;
using Orders.Domain.Interfaces.Services;
using Orders.Infrastructure;
using Orders.Infrastructure.ExternalServices;
using Orders.Infrastructure.ExternalServices.Configuration;
using Orders.Infrastructure.MessageBus.Configuration;
using Orders.Infrastructure.Persistence.Repositories;
using System.Text;

namespace Orders.API.Middlewares
{
    public static class ApplicationModule
    {
        public const int DEFAULT_PAGE_NUMBER = 1;
        public const int DEFAULT_PAGE_SIZE = 25;

        public static void AddCustomMiddlewares(this WebApplicationBuilder builder)
        {
            builder.ResolveDependencies();
            builder.ConfigureDataBase();
            builder.AddSecurityConfig();
            builder.AddDocumentationConfig();
            builder.Services.AddMessageBus(builder.Configuration.GetSection(nameof(BusSettingsConfiguration))["Hostname"] ?? string.Empty,
            builder.Configuration.GetSection(nameof(BusSettingsConfiguration))["ClientProvidedName"] ?? string.Empty);
        }

        public static void ResolveDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IVoucherRepository, VoucherRepository>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IStripeService, StripeService>();
            builder.Services.Configure<StripeConfiguration>(builder.Configuration.GetSection(nameof(StripeConfiguration)));
            builder.Services.Configure<BusSettingsConfiguration>(builder.Configuration.GetSection(nameof(BusSettingsConfiguration)));
        }

        public static void ConfigureDataBase(this WebApplicationBuilder builder) =>
            builder.Services.AddDbContext<OrdersDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty));

        public static void AddSecurityConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
                };
            });

            builder.Services.AddAuthorization();
        }

        public static void ConfigureDevEnvironment(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapSwagger().RequireAuthorization();
        }

        public static void UseSecurity(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
