using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Teste.Sale.Ambev.Aplication.Commands.CancelSale;
using Teste.Sale.Ambev.Aplication.Commands.CreateSale;
using Teste.Sale.Ambev.Aplication.Queries;
using Teste.Sale.Ambev.Domain.Repositories;
using Teste.Sale.Ambev.Driven.InMemory;
using Teste.Sale.Ambev.Infrastructure.Data;
using Teste.Sale.Ambev.Infrastructure.Repositories;

namespace Teste.Sale.Ambev.Crosscutting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Handlers
            services.AddSingleton<ISaleRepository, InMemorySaleRepository>();
            services.AddScoped<CreateSaleCommandHandler>();
            services.AddScoped<GetSaleByIdHandler>();
            services.AddScoped<GetAllSalesHandler>();
            services.AddScoped<CancelSaleHandler>();
            services.AddScoped<CancelSaleItemCommandHandler>();



            //DB ou Mock
            var connectionString = configuration.GetConnectionString("SalesDb");

            if (!string.IsNullOrEmpty(connectionString))
            {
                services.AddDbContext<SalesDbContext>(options =>
                    options.UseSqlServer(connectionString));
                services.AddScoped<ISaleRepository, SaleEfRepository>();
            }
            else
            {
                services.AddSingleton<ISaleRepository, InMemorySaleRepository>();
            }

            return services;
        }
    }
}