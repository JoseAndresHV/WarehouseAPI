using WarehouseAPI.ApplicationCore.Interfaces.Repositories;

namespace WarehouseAPI.ApplicationCore.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        ISaleRepository Sales { get; }
    }
}
