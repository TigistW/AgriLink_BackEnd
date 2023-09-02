using System.Security.Claims;
using Api.Interfaces;
using Api.Services;
using Api.UserDtos;
using Application.Responses;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseApiController
{

    private readonly IAuthService _authService;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMediator mediator, IMapper mapper)
    {
        _authService = authService;
        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var response = await _authService.Login(loginDto);
        return HandleResult(response);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<Result<RegisterResponse>>> Register([FromBody] RegisterDto registerDto)
    {
        var response = await _authService.Register(_mapper.Map<RegisterDto>(registerDto));
        return HandleResult(response);
    }


    [AllowAnonymous]
    [HttpPost("delete_user")]
    public async Task<ActionResult<Result<bool>>> DeleteUserByEmail([FromBody] DeleUserDto deleteUserDto)
    {
        var response = await _authService.DeleteUser(_mapper.Map<DeleUserDto>(deleteUserDto));
        return HandleResult(response);
    }














    // [Authorize]
    // [HttpGet]
    // public async Task<ActionResult<UserDto>> GetCurrentUser()
    // {
    //     var user = await _userManager.Users
    //     .FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));

    //     if (user == null) return Unauthorized();

    //     // return HandleResult(Result<UserDto>.Success(CreateUserObject(user)));
    // }
}

