using PRN231.Domain.Exceptions.Common;

namespace PRN231.Domain.Exceptions.User;

public sealed class UserNotFoundException() : NotFoundException("User not found!")
{
}
