using System.ComponentModel.DataAnnotations;

namespace WarehouseAPI.Web.Dtos
{
    public class SaleDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public decimal Subtotal { get; set; }

        [Required]
        public decimal Iva { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
