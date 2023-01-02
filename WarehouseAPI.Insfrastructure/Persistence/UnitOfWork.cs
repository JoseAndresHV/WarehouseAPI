using WarehouseAPI.ApplicationCore.Interfaces;
using WarehouseAPI.ApplicationCore.Interfaces.Repositories;

namespace WarehouseAPI.Insfrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IProductRepository products, ISaleRepository sales)
        {
            Products = products;
            Sales = sales;
        }

        public IProductRepository Products { get; set; }
        public ISaleRepository Sales { get; set; }
    }
}
