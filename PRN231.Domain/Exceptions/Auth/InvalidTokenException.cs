using PRN231.Domain.Exceptions.Common;

namespace PRN231.Domain.Exceptions.Auth;

public class InvalidTokenException() : BadRequestException("Invalid token!")
{
}
