using PRN231.Domain.Exceptions.Common;

namespace PRN231.Domain.Exceptions.Auth;

public class WrongCredentialsException() : UnauthorizedException("Wrong credentials!")
{
}
