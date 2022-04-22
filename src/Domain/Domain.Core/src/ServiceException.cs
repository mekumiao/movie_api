namespace MovieAPI.Domain.Core;

public class ServiceException : ApplicationException
{
    public ServiceException(string message) : base(message)
    {

    }
}
