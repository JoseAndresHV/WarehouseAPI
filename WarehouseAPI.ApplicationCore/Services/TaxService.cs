using WarehouseAPI.ApplicationCore.Entities;
using WarehouseAPI.ApplicationCore.Enums;
using WarehouseAPI.ApplicationCore.Interfaces.Services;

namespace WarehouseAPI.ApplicationCore.Services
{
    public class TaxService : ITaxService
    {
        public Sale CalculateTaxAndGetSale(Product product, int qty, TaxType taxType)
        {
            var subtotal = product.Price * qty;
            var tax = subtotal * TaxTypes.Values[taxType];

            return new Sale
            {
                ProductId = product.Id.GetValueOrDefault(),
                Qty = qty,
                Subtotal = subtotal,
                Iva = tax,
                Total = subtotal + tax,
                DateTime = DateTime.Now
            };
        }
    }
}
