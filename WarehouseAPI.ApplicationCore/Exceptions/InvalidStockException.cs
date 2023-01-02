namespace WarehouseAPI.ApplicationCore.Exceptions
{
    public class InvalidStockException : Exception
    {
        public InvalidStockException(int stock) :
            base($"The stock amount {stock} is not valid.")
        {
        }
    }
}
