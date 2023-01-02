namespace WarehouseAPI.ApplicationCore.Entities
{
    public class Sale
    {
        public int? Id { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public DateTime DateTime { get; set; }
    }
}
