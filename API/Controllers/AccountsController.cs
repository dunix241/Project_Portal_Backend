using System.Security.Claims;
using API.DTOs.Accounts;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[Controller]")]
public class AccountsController : ControllerBase
{
    private readonly DataContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly TokenService _tokenService;
    private readonly UserManager<User> _userManager;

    public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager,
        TokenService tokenService, DataContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _context = context;
    }

    [HttpPost]
    [Route("[Action]")]
    public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO loginRequestDto)
    {
        var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

        if (user == null) return Unauthorized();

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, false);

        if (result.Succeeded)
        {
            var userDto = CreateUserDto(user);

            var userWithAvatar = await _context
                .Users
                .Include(usr => usr.Avatar)
                .FirstOrDefaultAsync(usr => usr.Id == user.Id);
            if (userWithAvatar?.Avatar != null)
            {
                var avt = userWithAvatar.Avatar;
                userDto.Avatar = avt.Url + '/' + avt.Name + '.' + avt.Extension;
            }

            return userDto;
        }

        return Unauthorized();
    }

    [HttpPost]
    [Route("[Action]")]
    public async Task<ActionResult<LoginResponseDTO>> Register(RegisterRequestDTO registerRequestDto)
    {
        if (await _userManager.Users.AnyAsync(user => user.Email == registerRequestDto.Email))
            ModelState.AddModelError("email", "This email address already exists");

        if (await _userManager.Users.AnyAsync(user => user.UserName == registerRequestDto.UserName))
            ModelState.AddModelError("username", "This username already exists");

        if (ModelState.ErrorCount != 0) return ValidationProblem();

        var user = new User
        {
            UserName = registerRequestDto.UserName,
            Email = registerRequestDto.Email,
            Name = registerRequestDto.Name,
            Address = registerRequestDto.Address
        };

        var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
        if (result.Succeeded) return CreateUserDto(user);

        return BadRequest("Problem registering user");
    }

    [Authorize]
    [HttpGet]
    [Route("[Action]")]
    public async Task<ActionResult<LoginResponseDTO>> GetCurrentUser()
    {
        var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
        return CreateUserDto(user);
    }

    private LoginResponseDTO CreateUserDto(User user)
    {
        var userDto = new LoginResponseDTO
        {
            Name = user.Name,
            Address = user.Address,
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user),
            Email = user.Email
        };
        if (user.Avatar != null)
            userDto.Avatar = user.Avatar.Url + '/' + user.Avatar.Name + '.' + user.Avatar.Extension;

        return userDto;
    }
}