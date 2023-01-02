using WarehouseAPI.ApplicationCore.Entities;

namespace WarehouseAPI.ApplicationCore.Interfaces.Services
{
    public interface ISaleService
    {
        Task<List<Sale>> GetAllSales();
        Task<Sale> GetSaleById(int id);
        Task<Sale> SellProduct(int productId, int qty);
        Task<Sale> RefundProduct(int saleId);
    }
}
