using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WarehouseAPI.ApplicationCore.Entities;
using WarehouseAPI.ApplicationCore.Interfaces.Repositories;
using WarehouseAPI.Insfrastructure.Sql.Commands;

namespace WarehouseAPI.Insfrastructure.Persistence.Repository
{
    internal class SaleRepository : ISaleRepository
    {
        public readonly string _connectionString;

        public SaleRepository(IConfiguration configuration)
        {
            _connectionString = configuration
                .GetConnectionString("DbConnection") ?? null!;
        }

        public async Task<IReadOnlyList<Sale>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection
                    .QueryAsync<Sale>(SaleCommands.Select);

                return result.ToList();
            }
        }

        public async Task<Sale?> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection
                    .QuerySingleOrDefaultAsync<Sale>(
                        SaleCommands.SelectWhereId, new { Id = id });

                return result;
            }
        }

        public async Task<int> AddAsync(Sale entity)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection
                    .ExecuteScalarAsync<int>(SaleCommands.Insert, entity);

                return result;
            }
        }

        public async Task<bool> UpdateAsync(Sale entity)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection
                    .ExecuteAsync(SaleCommands.Update, entity);

                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection
                    .ExecuteAsync(SaleCommands.Delete, new { Id = id });

                return result > 0;
            }
        }
    }
}
