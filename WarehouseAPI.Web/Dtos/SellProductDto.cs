using System.ComponentModel.DataAnnotations;

namespace WarehouseAPI.Web.Dtos
{
    public class SellProductDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Qty { get; set; }
    }
}
