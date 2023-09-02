using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Interfaces;
using Api.Settings;
using Api.UserDtos;
using Application.Responses;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<User> userManager,
                       SignInManager<User> signInManager,
                       IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
    }

    private async Task<JwtSecurityToken> GenerateToken(User user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        Console.WriteLine(roles);
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {

            roleClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var Claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }.Union(userClaims)
         .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: Claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredential
        );

        return token;
    }

    public async Task<Result<LoginResponse>> Login(LoginDto request)
    {
        Result<LoginResponse> result = new Result<LoginResponse>();
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            result.IsSuccess = false;
            result.Error = $"User with given Email({request.Email}) doesn't exist";
            return result;
        }

        var res = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
        if (!res.Succeeded)
        {
            result.IsSuccess = false;
            result.Error = $"Incorrect password";
            return result;
        }

        JwtSecurityToken token = await GenerateToken(user);
        var response = new LoginResponse()
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };
        result.IsSuccess = true;
        result.Value = response;
        return result;
    }


    public async Task<Result<RegisterResponse>> Register(RegisterDto request)

    {
        var result = new Result<RegisterResponse>();
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            result.IsSuccess = false;
            result.Error = $"User with given Email({request.Email}) already exists";
            return result;
        }

        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            EmailConfirmed = false
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);

        if (!createResult.Succeeded)
        {
            result.IsSuccess = false;
            foreach (var Error in createResult.Errors)
            {
                result.Error = Error.Description;
            }
            return result;
        }

        var createdUser = await _userManager.FindByNameAsync(request.UserName);

        // Assign roles to the newly registered user
        if (request.Roles != null && request.Roles.Any())
        {
            var roleResult = await _userManager.AddToRolesAsync(createdUser, request.Roles);
            if (!roleResult.Succeeded)
            {
                result.IsSuccess = false;
                result.Error = "Failed to assign roles to the user.";
                return result;
            }
        }

        result.IsSuccess = true;
        result.Value = new RegisterResponse
        {
            UserId = createdUser.Id,
            Email = createdUser.Email,
            UserName = createdUser.UserName

        };

        return result;
    }

    public async Task<Result<bool>> DeleteUser(DeleUserDto deleteUserDto)
    {
        var user = await _userManager.FindByEmailAsync(deleteUserDto.Email);
        var result = new Result<bool>();
        if (user == null)
        {
            result.IsSuccess = false;
            result.Value = false;
            result.Message = $"User does not exist!";
            return result;
        }

        var res = await _userManager.DeleteAsync(user);
        result.IsSuccess = true;
        result.Value = res.Succeeded;
        result.Message = $"User deleted successfully!";
        return result;
    }

   

}
