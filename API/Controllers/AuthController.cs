using System.Security.Claims;
using API.DTOs.Accounts;
using API.Services;
using Application.Auth;
using Asp.Versioning;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[AllowAnonymous]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[Controller]")]
public class AuthController : BaseApiController
{
    private readonly DataContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly TokenService _tokenService;
    private readonly UserManager<User> _userManager;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager,
        TokenService tokenService, DataContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _context = context;
    }

    [HttpPost]
    [Route("[Action]")]
    [SwaggerOperation(Summary = "Log a user in")]
    public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO loginRequestDto)
    {
        var user = await Mediator.Send(new Login.Query{LoginRequestDto = loginRequestDto});

        if (user == null) return Unauthorized();

        var userDto = await CreateUserDto(user);
        userDto.Avatar = user.Avatar?.Url + '/' + user.Avatar?.Name + '.' + user.Avatar?.Extension;

        return userDto;
    }

    // [HttpPost]
    // [Route("[Action]")]
    // [SwaggerOperation(Summary = "Register a user")]
    // public async Task<ActionResult<LoginResponseDTO>> Register(RegisterRequestDTO registerRequestDto)
    // {
    //     var user = await Mediator.Send(new Register.Query{RegisterRequestDto = registerRequestDto});
    //     if (user == null) return BadRequest("Problem registering user");
    //     
    //     return await CreateUserDto(user);
    // }

    [Authorize]
    [HttpGet]
    [Route("[Action]")]
    [SwaggerOperation(Summary = "Get the current user using a login token")]
    public async Task<ActionResult<LoginResponseDTO>> GetCurrentUser()
    {
        var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
        return await CreateUserDto(user);
    }

    [HttpPatch("[Action]")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequestDTO requestDto)
    {
        return HandleResult(await Mediator.Send(new ResetPassword.Command { ResetPasswordRequestDto = requestDto }));
    }

    [HttpPatch("[Action]")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequestDTO requestDto)
    {
        return HandleResult(await Mediator.Send(new ChangePassword.Command { RequestDto = requestDto }));
    }
    
    private async Task<LoginResponseDTO> CreateUserDto(User user)
    {
        var userDto = new LoginResponseDTO
        {
            Name = user.Name,
            Address = user.Address,
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user),
            Email = user.Email,
            Roles = await _userManager.GetRolesAsync(user)
        };
        if (user.Avatar != null)
            userDto.Avatar = user.Avatar.Url + '/' + user.Avatar.Name + '.' + user.Avatar.Extension;

        return userDto;
    }
}
