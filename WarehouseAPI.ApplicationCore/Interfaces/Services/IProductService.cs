﻿using WarehouseAPI.ApplicationCore.Entities;

namespace WarehouseAPI.ApplicationCore.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
