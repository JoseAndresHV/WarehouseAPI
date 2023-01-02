using System.ComponentModel.DataAnnotations;

namespace WarehouseAPI.Web.Dtos
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string ProductName { get; set; } = null!;

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
