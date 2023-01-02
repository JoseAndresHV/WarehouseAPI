namespace WarehouseAPI.Insfrastructure.Sql.Commands
{
    public class SaleCommands
    {
        public static string Select =>
            @"SELECT * FROM Sale";

        public static string SelectWhereId =>
            @"SELECT * FROM Sale
              WHERE Id = @Id";

        public static string Insert =>
            @"INSERT INTO
              Sale(ProductId, Qty, Subtotal, Iva, Total, DateTime)
              OUTPUT INSERTED.Id
              VALUES(@ProductId, @Qty, @Subtotal, @Iva, @Total, @DateTime)";

        public static string Update =>
            @"UPDATE Sale SET
                ProductId = @ProductId, 
                Qty = @Qty, 
                Subtotal = @Subtotal,
                Iva = @Iva, 
                Total = @Total,
                DateTime = @DateTime
              WHERE Id = @Id";

        public static string Delete =>
            @"DELETE FROM Sale
              WHERE Id = @Id";
    }
}
