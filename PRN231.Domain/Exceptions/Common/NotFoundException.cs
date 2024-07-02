namespace PRN231.Domain.Exceptions.Common;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Not found!")
    {
        
    }

    public NotFoundException(string message) : base(message)
    {
        
    }
}
