namespace WarehouseAPI.ApplicationCore.Exceptions
{
    public class MissingIdException<T> : Exception where T : class
    {
        public MissingIdException() :
            base($"The {nameof(T)} Id was not provided.")
        {
        }
    }
}
