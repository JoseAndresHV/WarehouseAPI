namespace WarehouseAPI.ApplicationCore.Entities
{
    public class Product
    {
        public int? Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
