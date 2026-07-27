namespace ITServiceManager.API.Middlewares
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) 
            : base(message)
        {
        }
    }
}
