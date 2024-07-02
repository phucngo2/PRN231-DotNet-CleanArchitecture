namespace PRN231.Domain.Exceptions.Common;

public abstract class UnauthorizedException(string message) : Exception(message)
{
}
