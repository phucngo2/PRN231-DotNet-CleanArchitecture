namespace PRN231.Domain.Exceptions.Common;

public class BadRequestException(string message) : Exception(message)
{
}
