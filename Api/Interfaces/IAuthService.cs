using Api.UserDtos;
using Application.Responses;


namespace Api.Interfaces;

public interface IAuthService
{
    public Task<Result<RegisterResponse>> Register(RegisterDto request);

    public Task<Result<LoginResponse>> Login(LoginDto request);

    // public Task<Result<string>> sendConfirmEmailLink(string Email);

    // public  Task<Result<string>> ConfirmEmail(string token, string email);

    // public  Task<Result<string>> ForgotPassword(string Email);

    // public  Task<Result<string>> ResetPassword(ResetPasswordModel resetPasswordModel);

    public Task<Result<bool>> DeleteUser(DeleUserDto deleteUserDto);
}
