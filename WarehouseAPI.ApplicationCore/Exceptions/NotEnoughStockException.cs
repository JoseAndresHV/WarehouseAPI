namespace WarehouseAPI.ApplicationCore.Exceptions
{
    public class NotEnoughStockException : Exception
    {
        public NotEnoughStockException(string product, int qty) :
            base($"There is not enough stock for {qty} {product}.")
        {
        }
    }
}
