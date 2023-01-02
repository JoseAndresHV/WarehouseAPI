using WarehouseAPI.ApplicationCore.Entities;
using WarehouseAPI.ApplicationCore.Exceptions;
using WarehouseAPI.ApplicationCore.Interfaces;
using WarehouseAPI.ApplicationCore.Interfaces.Services;

namespace WarehouseAPI.ApplicationCore.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return (await _unitOfWork.Products.GetAllAsync()).ToList();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product is null)
            {
                throw new ProductNotFoundException(id);
            }

            return product!;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            if (product.Stock <= 0)
            {
                throw new InvalidStockException(product.Stock);
            }

            if (product.Price <= 0)
            {
                throw new InvalidPriceException(product.Price);
            }

            product.Id = await _unitOfWork.Products.AddAsync(product);
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            if (product.Id is null)
            {
                throw new MissingIdException<Product>();
            }

            if (product.Stock <= 0)
            {
                throw new InvalidStockException(product.Stock);
            }

            if (product.Price <= 0)
            {
                throw new InvalidPriceException(product.Price);
            }

            var updated = await _unitOfWork.Products.UpdateAsync(product);
            if (!updated)
            {
                throw new ProductNotFoundException(product.Id.GetValueOrDefault());
            }

            return product;
        }

        public async Task DeleteProduct(int id)
        {
            var deleted = await _unitOfWork.Products.DeleteAsync(id);
            if (!deleted)
            {
                throw new ProductNotFoundException(id);
            }
        }
    }
}
