using WarehouseAPI.ApplicationCore.Entities;
using WarehouseAPI.ApplicationCore.Enums;
using WarehouseAPI.ApplicationCore.Exceptions;
using WarehouseAPI.ApplicationCore.Interfaces;
using WarehouseAPI.ApplicationCore.Interfaces.Services;

namespace WarehouseAPI.ApplicationCore.Services
{
    public class SaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITaxService _taxService;

        public SaleService(IUnitOfWork unitOfWork, ITaxService taxService)
        {
            _unitOfWork = unitOfWork;
            _taxService = taxService;
        }

        public async Task<List<Sale>> GetAllSales()
        {
            return (await _unitOfWork.Sales.GetAllAsync()).ToList(); ;
        }

        public async Task<Sale> GetSaleById(int id)
        {
            var sale = await _unitOfWork.Sales.GetByIdAsync(id);
            if (sale is null)
            {
                throw new SaleNotFoundException(id);
            }

            return sale!;
        }

        public async Task<Sale> SellProduct(int productId, int qty)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product is null)
            {
                throw new ProductNotFoundException(productId);
            }

            if (qty > product.Stock || product.Stock <= 0)
            {
                throw new NotEnoughStockException(product.ProductName, qty);
            }

            product!.Stock -= qty;
            await _unitOfWork.Products.UpdateAsync(product);

            var sale = _taxService.CalculateTaxAndGetSale(product, qty, TaxType.IVA);
            sale.Id = await _unitOfWork.Sales.AddAsync(sale);

            return sale;
        }

        public async Task<Sale> RefundProduct(int saleId)
        {
            var sale = await _unitOfWork.Sales.GetByIdAsync(saleId);
            if (sale is null)
            {
                throw new SaleNotFoundException(saleId);
            }

            var product = await _unitOfWork.Products.GetByIdAsync(sale.ProductId);
            if (product is null)
            {
                throw new ProductNotFoundException(sale.ProductId);
            }

            product.Stock += sale.Qty;
            await _unitOfWork.Products.UpdateAsync(product);

            await _unitOfWork.Sales.DeleteAsync(sale.Id.GetValueOrDefault());

            return sale;
        }
    }
}
