using WarehouseAPI.ApplicationCore.Entities;
using WarehouseAPI.ApplicationCore.Enums;

namespace WarehouseAPI.ApplicationCore.Interfaces.Services
{
    public interface ITaxService
    {
        Sale CalculateTaxAndGetSale(Product product, int qty, TaxType taxType);
    }
}
