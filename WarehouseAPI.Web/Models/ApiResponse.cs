namespace WarehouseAPI.Web.Models
{
    public class ApiResponse<T> where T : class
    {
        public bool Success { get; set; } = false;
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}
