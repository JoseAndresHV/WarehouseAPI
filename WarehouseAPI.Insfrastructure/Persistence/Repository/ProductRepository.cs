using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WarehouseAPI.ApplicationCore.Entities;
using WarehouseAPI.ApplicationCore.Interfaces.Repositories;
using WarehouseAPI.Insfrastructure.Sql.Commands;

namespace WarehouseAPI.Insfrastructure.Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration
                .GetConnectionString("DbConnection") ?? null!;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection
                    .QueryAsync<Product>(ProductCommands.Select);

                return result.ToList();
            }
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection
                    .QuerySingleOrDefaultAsync<Product>(
                        ProductCommands.SelectWhereId, new { Id = id });

                return result;
            }
        }

        public async Task<int> AddAsync(Product entity)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection
                    .ExecuteScalarAsync<int>(ProductCommands.Insert, entity);

                return result;
            }
        }

        public async Task<bool> UpdateAsync(Product entity)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection
                    .ExecuteAsync(ProductCommands.Update, entity);

                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection
                    .ExecuteAsync(ProductCommands.Delete, new { Id = id });

                return result > 0;
            }
        }
    }
}
