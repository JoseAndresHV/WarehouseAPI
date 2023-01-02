using Microsoft.Extensions.DependencyInjection;
using WarehouseAPI.ApplicationCore.Interfaces;
using WarehouseAPI.ApplicationCore.Interfaces.Repositories;
using WarehouseAPI.Insfrastructure.Persistence;
using WarehouseAPI.Insfrastructure.Persistence.Repository;

namespace WarehouseAPI.Insfrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
