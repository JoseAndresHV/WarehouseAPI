namespace WarehouseAPI.ApplicationCore.Exceptions
{
    public class InvalidQtyException : Exception
    {
        public InvalidQtyException(int qty) :
            base($"The quantity {qty} is not valid.")
        {
        }
    }
}
