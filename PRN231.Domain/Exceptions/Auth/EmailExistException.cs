using PRN231.Domain.Exceptions.Common;

namespace PRN231.Domain.Exceptions.Auth;

public sealed class EmailExistException() : ConflictException("Email exist!")
{   
}
