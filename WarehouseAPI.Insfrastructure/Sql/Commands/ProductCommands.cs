namespace WarehouseAPI.Insfrastructure.Sql.Commands
{
    public class ProductCommands
    {
        public static string Select =>
            @"SELECT * FROM Product";

        public static string SelectWhereId =>
            @"SELECT * FROM Product
              WHERE Id = @Id";

        public static string Insert =>
            @"INSERT INTO 
              Product(ProductName, Description, Stock, Price)
              OUTPUT INSERTED.Id
              VALUES(@ProductName, @Description, @Stock, @Price)";

        public static string Update =>
            @"UPDATE Product SET
                ProductName = @ProductName,
                Description = @Description,
                Stock = @Stock,
                Price = @Price
              WHERE Id = @Id";

        public static string Delete =>
            @"DELETE FROM Product
              WHERE Id = @Id";
    }
}
