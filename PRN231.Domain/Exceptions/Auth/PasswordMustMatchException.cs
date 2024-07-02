using PRN231.Domain.Exceptions.Common;

namespace PRN231.Domain.Exceptions.Auth;

public sealed class PasswordMustMatchException() : BadRequestException("Password must match!")
{
}
