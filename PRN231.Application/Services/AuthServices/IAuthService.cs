using PRN231.Application.Services.AuthServices.Dtos;

namespace PRN231.Application.Services.AuthServices;

public interface IAuthService
{
    public Task SignUpAsync(SignUpRequestDto request);
    public Task<LogInResponseDto> LoginAsync(LogInRequestDto request);
    public Task PermanentlyDeleteUserAsync(PermanentlyDeleteRequestDto request);
    public Task UpdatePasswordAsync(UpdatePasswordRequestDto request);
    public Task RequestResetPasswordAsync(ForgotPasswordRequestDto request);
    public Task<bool> VerifyResetTokenAsync(VerifyResetTokenRequestDto request);
    public Task ResetPasswordAsync(ResetPasswordRequestDto request);
}
