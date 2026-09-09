namespace CareHomeApi.Exceptions;

public class DuplicateBillException : Exception
{
    public DuplicateBillException(string message) : base(message)
    {
    }
}
