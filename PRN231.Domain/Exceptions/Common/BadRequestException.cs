namespace PRN231.Domain.Exceptions.Common;

public abstract class BadRequestException(string message) : Exception(message)
{
}
